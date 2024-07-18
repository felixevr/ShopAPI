using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetCartWithProducts() 
        {
            var response = await _cartApplication.ListCartsWithProducts();

            return Ok(response);
        }
    }
}
