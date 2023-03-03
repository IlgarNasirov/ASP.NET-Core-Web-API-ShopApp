using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Models;

namespace ShopApp.Data
{
    public class ShopAppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ShopAppDbContext(DbContextOptions<ShopAppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
