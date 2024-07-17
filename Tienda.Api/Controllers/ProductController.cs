using Microsoft.AspNetCore.Mvc;
using Tienda.Application.Dtos.Request;
using Tienda.Application.Interfaces;
using Tienda.Infrastructure.Commons.Bases.Request;

namespace Tienda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpPost]
        public async Task<IActionResult> ListProducts([FromBody] BaseFiltersRequest filters)
        {
            var response = await _productApplication.ListProducts(filters);
            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectProducts()
        {
            var response = await _productApplication.ListSelectProducts();
            return Ok(response);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _productApplication.ProductById(productId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct(ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProduct(productId);
            return Ok(response);
        }
    }
}

