using DapperTrial.API.Models.RequestModels;
using DapperTrial.API.Models.ResponseModels;
using DapperTrial.API.Services.Cart.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperTrial.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator mediator;
        public CartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("add-cart-to-get")]
        public async Task<AddCartResponseModel> AddCart(AddCartRequestModel request)
        {
            var command = new AddCartCommand() { requestModel = request };
            return await mediator.Send(command);
        }
    }
}
