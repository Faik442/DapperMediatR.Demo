using Dapper;
using DapperMediatR.Demo.API.Models;
using MediatR;
using System.Data;

namespace DapperMediatR.Demo.API.Services.User.Queries
{
    public class GetUserByIdQuery : IRequest<DapperMediatR.Demo.API.Models.User>
    {
        public long UserId { get; set; }
    }
    public class GetByIdQueryHandler : IRequestHandler<GetUserByIdQuery, DapperMediatR.Demo.API.Models.User>
    {
        private readonly IDbConnection _dbConnection;
        public GetByIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<DapperMediatR.Demo.API.Models.User> Handle(GetUserByIdQuery req, CancellationToken cancellationToken)
        {
            var query = @"Select * from ""Users"" where ""UserId"" = @UserId";
            var parameter = new
            {
                UserId = req.UserId,
            };
            return await _dbConnection.QuerySingleAsync<DapperMediatR.Demo.API.Models.User>(query, parameter);
            
        }
    }

}
