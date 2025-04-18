using Microsoft.EntityFrameworkCore;
using MSTempProj.ProjectAPI.Entities;

namespace MSTempProj.ProjectAPI.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
