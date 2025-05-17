using MSProductAPI.Entities;
using MSProductAPI.Messaging.Interfaces;
using MSProductAPI.Models.Events;
using MSProductAPI.Repositories.Interfaces;
using MSProductAPI.Services.Interfaces;

namespace MSProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public ProductService(IProductRepository repository, IRabbitMQPublisher rabbitMQPublisher)
        {
            _repository = repository;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product ?? throw new KeyNotFoundException("Product not found");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await _repository.GetAllAsync();

        public async Task<PaginatedResult<Product>> GetAllProductsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be at least 1");
            if (pageSize < 1 || pageSize > 100) throw new ArgumentException("Page size must be between 1-100");

            return await _repository.GetAllAsync(pageNumber, pageSize);
        }


        public async Task<Product> CreateProductAsync(Product product)
        {
            ValidateProduct(product);
            var createdProduct =  await _repository.CreateAsync(product);

            var productEvent = new ProductCreatedEvent
            {
                ProductId = createdProduct.Id,
                Name = createdProduct.Name,
                Price = createdProduct.Price,
                Stock = createdProduct.Inventory,
                CreatedAt = DateTime.UtcNow
            };
            await _rabbitMQPublisher.PublishProductCreatedAsync(productEvent);

            return createdProduct;
        }

        public async Task UpdateProductAsync(Product product)
        {
            ValidateProduct(product);
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id) =>
            await _repository.DeleteAsync(id);

        private void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required");

            if (product.Price <= 0)
                throw new ArgumentException("Price must be greater than zero");
        }
    }
}
