using Dapper;
using MediatR;
using System.Data;

namespace DapperTrial.API.Services.Product.Queries
{
    public class GetProductsByIdsQuery : IRequest<List<DapperTrial.API.Models.Product>>
    {
        public List<int> ProductIds;
    }
    public class GetProductsByIdsQueryHandler : IRequestHandler<GetProductsByIdsQuery, List<DapperTrial.API.Models.Product>>
    {
        private readonly IDbConnection _dbConnection;
        public GetProductsByIdsQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<DapperTrial.API.Models.Product>> Handle(GetProductsByIdsQuery req, CancellationToken cancellationToken)
        {
            var query = @"Select * from ""Products"" where ""ProductId"" = ANY(@ProductIds)";
            var parameters = new
            {
                ProductIds = req.ProductIds.ToArray()
            };
            var products = await _dbConnection.QueryAsync<DapperTrial.API.Models.Product>(query, parameters);
            return products.ToList();
        }
    }
}
