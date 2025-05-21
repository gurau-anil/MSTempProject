using System.ComponentModel.DataAnnotations;

namespace MSCartAPI.Entities
{
    public class Cart
    {
        public string CartId { get; set; } = string.Empty;

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
