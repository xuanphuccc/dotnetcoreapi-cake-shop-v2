using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class CakeShopContext : DbContext
    {
        public CakeShopContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Category)
                      .WithMany(p => p.Products)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasOne(pi => pi.Product)
                      .WithMany(p => p.Images)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.OrderStatus)
                      .WithMany(os => os.Orders)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(o => o.ShippingMethod)
                      .WithMany(sm => sm.Orders)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.Items)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(oi => new {oi.ProductId, oi.OrderId})
                      .IsUnique(true);
            });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
