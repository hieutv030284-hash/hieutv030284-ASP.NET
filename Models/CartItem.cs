using System.ComponentModel.DataAnnotations;

namespace AppleStoreWeb.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        
        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}