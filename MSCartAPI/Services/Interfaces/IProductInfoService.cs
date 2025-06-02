using MSCartAPI.Models;

namespace MSCartAPI.Services.Interfaces
{
    public interface IProductInfoService
    {
        Task<ProductDto?> GetProductByIdAsync(int productId);
    }
}
