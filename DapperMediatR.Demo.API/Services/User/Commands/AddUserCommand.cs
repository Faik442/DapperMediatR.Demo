using Dapper;
using MediatR;
using System.Data;

namespace DapperMediatR.Demo.API.Repositories.User.Commands
{
    public class AddUserCommand : IRequest<bool>
    {
        public string? Username { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IDbConnection _dbConnection;

        public AddUserCommandHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var query = @"INSERT INTO ""Users"" (""UserName"", ""CreatedDate"") VALUES (@UserName, CURRENT_TIMESTAMP)";

            var parameters = new
            {
                UserName = request.Username
            };

            var result = await _dbConnection.ExecuteAsync(query, parameters);
            return result > 0;
        }
    }
}
