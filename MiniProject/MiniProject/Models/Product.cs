using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        [Url(ErrorMessage = "Invalid format of url")]
        public string? Image { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public virtual Category? category { get; set; }
    }
}
