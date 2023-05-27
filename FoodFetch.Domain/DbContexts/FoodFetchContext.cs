using FoodFetch.Domain.Database.Models;

using Microsoft.EntityFrameworkCore;

namespace FoodFetch.Domain.DbContexts
{
    public class FoodFetchContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<DatabaseRestaurant> Restaurants { get; set; }
        public DbSet<DatabaseProduct> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderDetails { get; set; }

        public FoodFetchContext()
        {
        }

        public FoodFetchContext(DbContextOptions<FoodFetchContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Order>()
                            .HasMany(p => p.Products)
                            .WithMany(o => o.Orders)
                            .UsingEntity<OrderProduct>(
                                b => _ = b.HasOne(x => x.Product).WithMany(),
                                b => _ = b.HasOne(x => x.Order).WithMany()
            );
        }
    }
}