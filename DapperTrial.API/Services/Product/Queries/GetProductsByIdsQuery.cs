using Dapper;
using MediatR;
using System.Data;

namespace DapperMediatR.Demo.API.Services.Product.Queries
{
    public class GetProductsByIdsQuery : IRequest<List<DapperMediatR.Demo.API.Models.Product>>
    {
        public List<int> ProductIds;
    }
    public class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, List<DapperMediatR.Demo.API.Models.Product>>
    {
        private readonly IDbConnection _dbConnection;
        public GetProductsByIdsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<DapperMediatR.Demo.API.Models.Product>> Handle(GetProductsByIdsQuery req, CancellationToken cancellationToken)
        {
            var query = @"Select * from ""Products"" where ""ProductId"" = ANY(@ProductIds)";
            var parameters = new
            {
                ProductIds = req.ProductIds.ToArray()
            };
            var products = await _dbConnection.QueryAsync<DapperMediatR.Demo.API.Models.Product>(query, parameters);
            return products.ToList();
        }
    }
}
