using System.ComponentModel.DataAnnotations;

namespace MSOrderAPI.Entities
{
    public class Order
    {
        public string OrderId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItem> Items { get; set; } = new();

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
    }
}
