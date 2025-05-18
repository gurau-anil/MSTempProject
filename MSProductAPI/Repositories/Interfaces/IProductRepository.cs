using MSProductAPI.Entities;

namespace MSProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<PaginatedResult<Product>> GetAllAsync(int pageNumber, int pageSize);

        Task<Product> CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
