using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppleStoreWeb.Models;
using System.Text.Json;

namespace AppleStoreWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Product ImageUrls as JSON
            builder.Entity<Product>()
                .Property(e => e.ImageUrls)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                    v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

            // Configure relationships
            builder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // Seed data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "iPhone", Description = "Điện thoại thông minh iPhone", ImageUrl = "/images/categories/iphone.jpg", CreatedAt = new DateTime(2024, 1, 1) },
                new Category { Id = 2, Name = "MacBook", Description = "Laptop MacBook", ImageUrl = "/images/categories/macbook.jpg", CreatedAt = new DateTime(2024, 1, 1) },
                new Category { Id = 3, Name = "iPad", Description = "Máy tính bảng iPad", ImageUrl = "/images/categories/ipad.jpg", CreatedAt = new DateTime(2024, 1, 1) },
                new Category { Id = 4, Name = "Apple Watch", Description = "Đồng hồ thông minh Apple Watch", ImageUrl = "/images/categories/apple-watch.jpg", CreatedAt = new DateTime(2024, 1, 1) },
                new Category { Id = 5, Name = "AirPods", Description = "Tai nghe không dây AirPods", ImageUrl = "/images/categories/airpods.jpg", CreatedAt = new DateTime(2024, 1, 1) }
            );

            // Seed Products
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    Description = "iPhone 15 Pro Max với chip A17 Pro, camera 48MP và khung Titanium cao cấp",
                    Price = 34999000,
                    OriginalPrice = 36999000,
                    ImageUrl = "/images/products/iphone-15-pro-max.jpg",
                    Stock = 50,
                    IsActive = true,
                    IsFeatured = true,
                    CategoryId = 1,
                    Specifications = "Chip A17 Pro, Camera 48MP, Pin 29 giờ, Khung Titanium",
                    Color = "Titanium Tự nhiên",
                    Storage = "256GB",
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new Product
                {
                    Id = 2,
                    Name = "iPhone 15 Pro",
                    Description = "iPhone 15 Pro với chip A17 Pro và camera Pro 48MP",
                    Price = 29999000,
                    ImageUrl = "/images/products/iphone-15-pro.jpg",
                    Stock = 45,
                    IsActive = true,
                    IsFeatured = true,
                    CategoryId = 1,
                    Specifications = "Chip A17 Pro, Camera 48MP, Pin 23 giờ",
                    Color = "Titanium Xanh",
                    Storage = "128GB",
                    CreatedAt = new DateTime(2024, 1, 2)
                },
                new Product
                {
                    Id = 3,
                    Name = "MacBook Air M3",
                    Description = "MacBook Air với chip M3 siêu mạnh mẽ",
                    Price = 27999000,
                    ImageUrl = "/images/products/macbook-air-m3.jpg",
                    Stock = 30,
                    IsActive = true,
                    IsFeatured = true,
                    CategoryId = 2,
                    Specifications = "Chip M3, 8GB RAM, 256GB SSD",
                    Color = "Bạc",
                    Storage = "256GB",
                    CreatedAt = new DateTime(2024, 1, 3)
                },
                new Product
                {
                    Id = 4,
                    Name = "iPad Pro",
                    Description = "iPad Pro với chip M4 và màn hình Liquid Retina XDR",
                    Price = 24999000,
                    ImageUrl = "/images/products/ipad-pro.jpg",
                    Stock = 20,
                    IsActive = true,
                    IsFeatured = true,
                    CategoryId = 3,
                    Specifications = "Chip M4, 8GB RAM, 256GB",
                    Color = "Bạc",
                    Storage = "256GB",
                    CreatedAt = new DateTime(2024, 1, 4)
                },
                new Product
                {
                    Id = 5,
                    Name = "AirPods Pro",
                    Description = "AirPods Pro với chống ồn chủ động",
                    Price = 6999000,
                    ImageUrl = "/images/products/airpods-pro.jpg",
                    Stock = 50,
                    IsActive = true,
                    IsFeatured = true,
                    CategoryId = 5,
                    Specifications = "Chip H2, Chống ồn chủ động, Pin 30 giờ",
                    CreatedAt = new DateTime(2024, 1, 5)
                }
            );
        }
    }
}