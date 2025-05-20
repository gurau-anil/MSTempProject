using System.ComponentModel.DataAnnotations;

namespace MSOrderAPI.Entities
{
    public class OrderItem
    {
        public string OrderItemId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public string OrderId { get; set; } = string.Empty;
    }
}
