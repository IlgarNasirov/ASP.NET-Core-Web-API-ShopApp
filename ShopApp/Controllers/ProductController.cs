using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DTOs;
using ShopApp.Repositories;
using ShopApp.Services;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet("products")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productRepository.GetProducts());
        }
        [HttpGet("products/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
                return BadRequest("Product with this id could not found!");
            return Ok(product);
        }
        [HttpPost("products")]
        public async Task<IActionResult> AddProduct([FromForm] ProductDTO productDTO)
        {
            var result = await _productRepository.AddProduct(productDTO);
            if (result == null)
                return BadRequest("The product could not be added!");
            return Ok(result);
        }
        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDTO productDTO)
        {
            var result = await _productRepository.UpdateProduct(id, productDTO);
            if (result.Type == "BAD")
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if (result.Type == "BAD")
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
    }
}
