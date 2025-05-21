namespace MSCartAPI.Entities
{
    public class CartItem
    {
        public string CartItemId { get; set; } = Guid.NewGuid().ToString();

        public string CartId { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
