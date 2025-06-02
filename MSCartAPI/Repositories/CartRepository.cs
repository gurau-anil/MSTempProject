using Microsoft.EntityFrameworkCore;
using MSCartAPI.Data;
using MSCartAPI.Entities;
using MSCartAPI.Repositories.Interfaces;

namespace MSCartAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;

        public CartRepository(CartDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByIdAsync(string cartId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }

        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
        }

        public async Task DeleteCartAsync(string cartId)
        {
            var cart = await GetCartByIdAsync(cartId);
            if (cart != null)
                _context.Carts.Remove(cart);
        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
        }

        public async Task RemoveCartItemAsync(string cartItemId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.CartItemId == cartItemId);
            if (item != null)
                _context.CartItems.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
