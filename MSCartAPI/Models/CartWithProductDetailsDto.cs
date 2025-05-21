namespace MSCartAPI.Models
{
    public class CartWithProductDetailsDto
    {
        public string CartId { get; set; }
        public List<CartItemWithProductDto> Items { get; set; } = new();
    }

}
