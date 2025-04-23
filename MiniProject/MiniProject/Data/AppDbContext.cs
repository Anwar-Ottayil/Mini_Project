using Microsoft.EntityFrameworkCore;
using MiniProject.Models;

namespace MiniProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> users { get; set; }
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasDefaultValue("user");
            modelBuilder.Entity<User>()
                .Property(x => x.IsBlocked)
                .HasDefaultValue("false");
            modelBuilder.Entity<Category>()
              .HasMany(x => x.products)
              .WithOne(r => r.category)
              .HasForeignKey(x => x.CategoryId);
            modelBuilder.Entity<Product>()
                .Property(pr => pr.Price).
                HasPrecision(18, 2);
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Title = "ZEBRONICS Zeb - Pixaplay 25 (5500 lm) Portable 1080p, 200-inch Screen Size, Quad Core, Auto Focus, Keystone, Obstacle Detection, Screenfit, Bluetooth, WiFi, HDMI-ARC, APP Support, Miracast Smart Projector  (Metallic Grey)", Description = "\r\nIntroducing the ZEB-PIXAPLAY 25, a smart LED projector that features 5500 lumens and supports screen sizes up to 508cm, as it ensures vibrant visuals in any setting. This projector offers seamless connectivity through BT v5.1, HDMI (ARC), USB, and AUX OUT, complemented by a remote control for easy operation. Enjoy FHD 1080p support and a 30,000-hour LED lamp lifespan, with built-in speakers for immersive audio. Equipped with a quad-core processor, dual-band connectivity, Miracast, iOS screen mirroring, and app support, it delivers a modern viewing experience. Advanced sensor tech including auto focus, auto keystone, auto obstacle detection, and auto screenfit enhance usability, making it ideal for diverse multimedia needs.", Price = 10999, Image = "https://rukminim2.flixcart.com/image/612/612/xif0q/projector/s/x/v/zeb-pixaplay-25-17-zeb-pixaplay-25-full-hd-zebronics-original-imah5f67nyepnnwv.jpeg?q=70", stock = 10, CategoryId = 1 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Electronics" }
                );

        }
    }
}
