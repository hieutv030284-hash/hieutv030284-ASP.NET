using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppleStoreWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? OriginalPrice { get; set; }
        
        public string? ImageUrl { get; set; }
        
        public List<string> ImageUrls { get; set; } = new List<string>();
        
        public int Stock { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public bool IsFeatured { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        // Foreign Key
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        // Navigation properties
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        
        // Specifications
        public string? Specifications { get; set; }
        public string? Color { get; set; }
        public string? Storage { get; set; }
    }
}