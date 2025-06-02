using MSCartAPI.Entities;
using MSCartAPI.Models;

namespace MSCartAPI.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetCartByIdAsync(string cartId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task AddCartAsync(Cart cart);
        Task AddItemToCartAsync(string cartId, CartItem cartItem);
        Task RemoveItemFromCartAsync(string cartId, string cartItemId);
        Task ClearCartAsync(string cartId);

        Task<CartWithProductDetailsDto?> GetCartWithProductDetailsAsync(string cartId);
    }
}
