using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Interfaces;

namespace Tienda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartApplication _cartApplication;
        public CartController(ICartApplication cartApplication)
        {
            _cartApplication = cartApplication;
        }

        [HttpGet("User{userId:int}")]
        public async Task<IActionResult> GetUserCartWithProducts(int userId) 
        {
            var response = await _cartApplication.ListCartsWithProducts(userId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCart([FromBody] CartRequestDto cartRequestDto)
        {
            var response = await _cartApplication.RegisterCart(cartRequestDto);

            return Ok(response);
        }

        [HttpGet("{cartId:int}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            var response = await _cartApplication.GetCartById(cartId);

            return Ok(response);
        }

        [HttpGet("WithProducts{cartId:int}")]
        public async Task<IActionResult> GetCartByIdWithProducts(int cartId)
        {
            var response = await _cartApplication.GetCartByIdWithProducts(cartId);

            return Ok(response);
        }
    }
}
