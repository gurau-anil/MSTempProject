using MSCartAPI.Entities;

namespace MSCartAPI.Repositories.Interfaces
{
    public interface ICartRepository
    {

        Task<Cart?> GetCartByIdAsync(string cartId);
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(string cartId);

        Task AddCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(string cartItemId);
        Task SaveChangesAsync();

    }
}
