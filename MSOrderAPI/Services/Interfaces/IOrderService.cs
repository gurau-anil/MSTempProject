using MSOrderAPI.Entities;
using MSOrderAPI.Models;

namespace MSOrderAPI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order?> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);

        Task<OrderWithProductDetailsDto?> GetOrderWithProductDetailsAsync(string orderId);
    }
}
