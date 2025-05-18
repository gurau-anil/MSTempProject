using MSProductAPI.Entities;

namespace MSProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<PaginatedResult<Product>> GetAllProductsAsync(int pageNumber, int pageSize);

        Task<Product> CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

    }
}
