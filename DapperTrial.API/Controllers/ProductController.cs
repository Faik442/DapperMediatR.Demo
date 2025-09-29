using DapperMediatR.Demo.API.Models.RequestModels;
using DapperMediatR.Demo.API.Models.ResponseModels;
using DapperMediatR.Demo.API.Repositories.User.Commands;
using DapperMediatR.Demo.API.Services.Product.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperMediatR.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-product")]
        public async Task<AddProductResponseModel> AddUser(AddProductRequestModel req)
        {
            var command = new AddProductCommand() { request = req };
            return await mediator.Send(command);
        }
    }
}
