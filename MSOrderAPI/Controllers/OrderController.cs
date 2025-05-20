using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSOrderAPI.Entities;
using MSOrderAPI.Models;
using MSOrderAPI.Services.Interfaces;

namespace MSOrderAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<ActionResult<Order>> GetOrder([FromQuery] string orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { orderId = order.OrderId }, order);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateOrder([FromBody] Order order)
        {
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<ActionResult> DeleteOrder([FromQuery] string orderId)
        {
            await _orderService.DeleteOrderAsync(orderId);
            return NoContent();
        }

        [HttpGet]
        [Route("get-with-products")]
        public async Task<ActionResult<OrderWithProductDetailsDto>> GetOrderWithProducts([FromQuery] string orderId)
        {
            var order = await _orderService.GetOrderWithProductDetailsAsync(orderId);
            if (order == null)
                return NotFound();
            return Ok(order);
        }
    }
}
