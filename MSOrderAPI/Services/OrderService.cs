using MSOrderAPI.Entities;
using MSOrderAPI.Models;
using MSOrderAPI.Repositories.Interfaces;
using MSOrderAPI.Services.Interfaces;

namespace MSOrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductInfoService _productInfoService;
        public OrderService(IOrderRepository repository, IProductInfoService productInfoService)
        {
            _repository = repository;
            _productInfoService = productInfoService;
        }

        public async Task<Order?> GetOrderByIdAsync(string orderId)
        {
            return await _repository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _repository.AddOrderAsync(order);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _repository.UpdateOrderAsync(order);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(string orderId)
        {
            await _repository.DeleteOrderAsync(orderId);
            await _repository.SaveChangesAsync();
        }

        public async Task<OrderWithProductDetailsDto?> GetOrderWithProductDetailsAsync(string orderId)
        {
            var order = await _repository.GetOrderByIdAsync(orderId);
            if (order == null)
                return null;

            var detailedItems = new List<OrderItemWithProductDto>();
            foreach (var item in order.Items)
            {
                var product = await _productInfoService.GetProductByIdAsync(item.ProductId);
                detailedItems.Add(new OrderItemWithProductDto
                {
                    OrderItemId = item.OrderItemId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Product = product
                });
            }

            return new OrderWithProductDetailsDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = detailedItems
            };
        }
    }
}
