using Microsoft.EntityFrameworkCore;
using MSCartAPI.Entities;

namespace MSCartAPI.Data
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
