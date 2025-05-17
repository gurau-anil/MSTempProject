using Microsoft.EntityFrameworkCore;
using MSTempProj.ProductAPI.Entities;

namespace MSTempProj.ProductAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
