using MSOrderAPI.Entities;

namespace MSOrderAPI.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string orderId);
        Task SaveChangesAsync();
    }
}
