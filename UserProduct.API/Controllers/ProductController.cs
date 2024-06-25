using Microsoft.AspNetCore.Mvc;
using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;

namespace UserProduct.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            var result = await _productService.CreateProduct(createProductDto);
            if (result.IsFailure) return BadRequest(result.Errors);
            
            var createdProduct = result.Data;
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct }, createdProduct);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto updateProductDto)
        {
            var result = await _productService.UpdateProduct(id, updateProductDto);
            if (result.IsFailure) return NotFound(result.Errors);
            
            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var result = await _productService.GetProductById(id);
            if (result.IsFailure) return NotFound(result.Errors);
            
            return Ok(result.Data);
        }


        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string searchTerm, [FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _productService.SearchProducts(searchTerm, paginationFilter);
            return Ok(result);
        }


        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationFilter paginationFilter)
        {
            var result = await _productService.GetAllProducts(paginationFilter);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
