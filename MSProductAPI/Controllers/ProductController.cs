using Microsoft.AspNetCore.Mvc;
using MSProductAPI.Entities;
using MSProductAPI.Services.Interfaces;

namespace MSProductAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("get-paged")]
        public async Task<IActionResult> GetPagedProduct( [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _productService.GetAllProductsAsync(pageNumber, pageSize);
                return Ok(new
                {
                    result.PageNumber,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    Data = result.Data
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                var createdProduct = await _productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProduct),
                    new { id = createdProduct.Id }, createdProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
