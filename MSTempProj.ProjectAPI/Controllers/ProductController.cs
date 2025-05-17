using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSTempProj.ProductAPI.Entities;
using MSTempProj.ProductAPI.Repositories.Interfaces;

namespace MSTempProj.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ProductApiScope")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            _logger.LogInformation("Getting all products");
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            _logger.LogInformation($"Getting product with id: {id}");
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")] // Only Admins and Super Admins can create
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            _logger.LogInformation($"Creating a new product: {product.Name}");
            await _productRepository.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")] // Only Admins and Super Admins can update
        public async Task<IActionResult> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _logger.LogInformation($"Updating product with id: {id}");
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")] // Only Admins and Super Admins can delete
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting product with id: {id}");
            await _productRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
