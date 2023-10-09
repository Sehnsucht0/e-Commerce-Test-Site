using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace e_Commerce_Test_Site.Models
{
    public class StoreUserContext: IdentityDbContext<User>
    {
        public StoreUserContext(DbContextOptions<StoreUserContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<CartItems> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserOrder>()
                .HasKey(a => new { a.ProductId, a.OrderId });

            builder.Entity<UserOrder>()
                .HasOne(a => a.Product)
                .WithMany(b => b.UserOrders)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserOrder>()
                .HasOne(a => a.Order)
                .WithMany(b => b.UserOrders)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Order>()
                .HasOne(a => a.UserData)
                .WithMany(b => b.Orders)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItems>()
                .HasOne(a => a.UserData)
                .WithMany(b => b.CartItems)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasOne(a => a.UserData)
                .WithOne(b => b.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Product>(entity =>
            {
                entity.Property(a => a.Price).HasColumnType("decimal(6, 2)");
                entity.Property(a => a.DiscountPercentage).HasColumnType("decimal(6, 2)");
                entity.Property(a => a.Rating).HasColumnType("decimal(6, 2)");
            });

            builder.Entity<Order>(entity => entity.Property(a => a.Total).HasColumnType("decimal(6,2)"));
        }
    }
}
