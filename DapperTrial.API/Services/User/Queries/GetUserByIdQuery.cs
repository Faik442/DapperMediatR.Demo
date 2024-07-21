using Dapper;
using DapperTrial.API.Models;
using MediatR;
using System.Data;

namespace DapperTrial.API.Services.User.Queries
{
    public class GetUserByIdQuery : IRequest<DapperTrial.API.Models.User>
    {
        public long UserId { get; set; }
    }
    public class GetByIdQueryHandler : IRequestHandler<GetUserByIdQuery, DapperTrial.API.Models.User>
    {
        private readonly IDbConnection _dbConnection;
        public GetByIdQueryHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<DapperTrial.API.Models.User> Handle(GetUserByIdQuery req, CancellationToken cancellationToken)
        {
            var query = @"Select * from ""Users"" where ""UserId"" = @UserId";
            var parameter = new
            {
                UserId = req.UserId,
            };
            return await _dbConnection.QuerySingleAsync<DapperTrial.API.Models.User>(query, parameter);
            
        }
    }

}
