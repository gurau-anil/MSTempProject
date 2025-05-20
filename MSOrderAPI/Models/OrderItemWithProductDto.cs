namespace MSOrderAPI.Models
{
    public class OrderItemWithProductDto
    {
        public string OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductDto? Product { get; set; }
    }
}
