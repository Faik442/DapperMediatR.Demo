using DapperMediatR.Demo.API.Models;
using DapperMediatR.Demo.API.Repositories.User.Commands;
using DapperMediatR.Demo.API.Services.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperMediatR.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-user")]
        public async Task<bool> AddUser(string userName)
        {
            var command = new AddUserCommand() { Username = userName };
            return await mediator.Send(command);
        }
        [HttpGet("get-by-id")]
        public async Task<User> GetById(long userId)
        {
            var query = new GetUserByIdQuery() { UserId = userId};
            return await mediator.Send(query);
        }
    }
}
