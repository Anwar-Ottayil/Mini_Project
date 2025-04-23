using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual ICollection<Product>? products { get; set; }
    }
}
