using Dapper;
using DapperMediatR.Demo.API.Models.RequestModels;
using DapperMediatR.Demo.API.Models.ResponseModels;
using MediatR;
using System.Data;
using System.Threading.Tasks;

namespace DapperMediatR.Demo.API.Services.Product.Commands
{
    public class AddProductCommand : IRequest<AddProductResponseModel>
    {
        public AddProductRequestModel? request { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponseModel>
    {
        private readonly IDbConnection _dbConnection;

        public AddProductCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<AddProductResponseModel> Handle(AddProductCommand req, CancellationToken cancellationToken)
        {
            var query = @"INSERT INTO ""Products"" (""ProductName"",""Price"", ""CreatedDate"", ""UpdatedDate"") 
                                VALUES (@ProductName, @Price, CURRENT_TIMESTAMP,CURRENT_TIMESTAMP) RETURNING ""ProductId"";";

            var parameters = new
            {
                ProductName = req.request!.ProductName,
                Price = req.request.Price,

            };

            var productId = await _dbConnection.QuerySingleAsync<int>(query, parameters);

            return new AddProductResponseModel
            {
                ProductName = req.request.ProductName,
                Price = req.request.Price,
                ProductId = productId
            };
        }
    }
}
