using MSOrderAPI.Models;

namespace MSOrderAPI.Services.Interfaces
{
    public interface IProductInfoService
    {
        Task<ProductDto?> GetProductByIdAsync(int productId);
    }
}
