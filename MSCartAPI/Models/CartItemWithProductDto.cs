namespace MSCartAPI.Models
{
    public class CartItemWithProductDto
    {
        public string CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDto? Product { get; set; }
    }

}
