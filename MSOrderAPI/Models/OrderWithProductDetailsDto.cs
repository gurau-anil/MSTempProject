namespace MSOrderAPI.Models
{
    public class OrderWithProductDetailsDto
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemWithProductDto> Items { get; set; } = new();
    }
}
