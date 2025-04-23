using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Password must be at least 6 characters long", MinimumLength = 6)]
        [MinLength(6, ErrorMessage = "Password must be above 6 characters")]
        [RegularExpression(@"^(?=.[A-Za-z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string Password { get; set; }
        public string? Role { get; set; }
        public bool IsBlocked { get; set; }
        public virtual Cart? Cart { get; set; }
    }
}
