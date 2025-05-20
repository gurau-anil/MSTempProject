using Microsoft.EntityFrameworkCore;
using MSOrderAPI.Data;
using MSOrderAPI.Entities;
using MSOrderAPI.Repositories.Interfaces;

namespace MSOrderAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task DeleteOrderAsync(string orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
                _context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
