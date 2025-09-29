using Azure.Core;
using Dapper;
using DapperMediatR.Demo.API.Models;
using DapperMediatR.Demo.API.Models.RequestModels;
using DapperMediatR.Demo.API.Models.ResponseModels;
using DapperMediatR.Demo.API.Services.Product.Queries;
using DapperMediatR.Demo.API.Services.Redis;
using DapperMediatR.Demo.API.Services.User.Queries;
using MediatR;
using System.Data;
using System.Text.Json;

namespace DapperMediatR.Demo.API.Services.Cart.Commands
{
    public class AddCartCommand : IRequest<AddCartResponseModel>
    {
        public AddCartRequestModel? requestModel { get; set; }
    }
    public class AddCartCommandHandler : IRequestHandler<AddCartCommand, AddCartResponseModel>
    {
        private readonly IDbConnection _dbConnection;
        private readonly IMediator _mediator;

        public AddCartCommandHandler(IDbConnection dbConnection, IMediator mediator)
        {
            _dbConnection = dbConnection;
            _mediator = mediator;
        }
        public async Task<AddCartResponseModel> Handle(AddCartCommand req, CancellationToken cancellationToken)
        {
            // redis e 2 tane value kayıtlı, request ve response json ları
            // client tan gelen request le redis deki aynıysa kayıtlı response value su döner
            // request ler eşleşmezse eski request ve response json ları silinir yenileri yaratılır
            var redisResponse = GetCartResponseRedis(req.requestModel!);
            if (redisResponse != null) return redisResponse;

            //Product ların get edildiği yer
            var getProducts = await _mediator.Send(new GetProductsByIdsQuery() { ProductIds = req.requestModel!.ProductIds});

            var totalPrice = getProducts.Sum(x => x.Price);
            var cartQuery = @"INSERT INTO ""Carts"" (""UserId"", ""TotalPrice"", ""CreatedDate"") 
                                                VALUES (@UserId, @TotalPrice, CURRENT_TIMESTAMP) RETURNING ""CartId"";";
            var parameters = new
            {
                UserId = req.requestModel.UserId,
                TotalPrice = totalPrice
            };
            var cartId = await _dbConnection.QuerySingleAsync<int>(cartQuery, parameters);
            var cartProductQuery = @"INSERT INTO ""CartProducts"" (""CartId"", ""ProductId"") VALUES (@CartId, @ProductId)";
            var cartProductParameters = getProducts.Select(x => new
            {
                CartId = cartId,
                ProductId = x.ProductId
            }).ToList();

            await _dbConnection.ExecuteAsync(cartProductQuery, cartProductParameters);

            //User ın get edildiği yer
            DapperMediatR.Demo.API.Models.User user = await _mediator.Send(new GetUserByIdQuery() { UserId = req.requestModel.UserId });

            var response = new AddCartResponseModel
            {
                Products = getProducts,
                TotalPrice = totalPrice,
                User = user
            };
            // En son response u redis e kaydedip return eder
            return RedisService.Add<AddCartResponseModel>(new AddRedisRequestModel<AddCartResponseModel>()
            {
                Action = response,
                ExpireTime = TimeSpan.FromDays(1),
                Key = $"resCart:{req.requestModel.UserId}"
            });

        }

        private AddCartResponseModel? GetCartResponseRedis(AddCartRequestModel req)
        {
            string reqRedisName = $"reqCart:{req.UserId}";
            string resRedisName = $"resCart:{req.UserId}";

            var redisReq = new AddRedisRequestModel<AddCartRequestModel>()
            {
                Action = req,
                ExpireTime = TimeSpan.FromDays(1),
                Key = reqRedisName
            };
            var request = RedisService.GetOrAdd(redisReq);
            if (JsonSerializer.Serialize(req) == JsonSerializer.Serialize(request))
            {
                return RedisService.Get<AddCartResponseModel>(resRedisName);
            }
            return null;
        }
    }

}
