using MSOrderAPI.Models;
using MSOrderAPI.Services.Interfaces;

namespace MSOrderAPI.Services
{
    public class ProductInfoService : IProductInfoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProductInfoService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUrl = config["ProductApi:BaseUrl"] ?? String.Empty;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/product/get?productId={productId}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }
    }
}
