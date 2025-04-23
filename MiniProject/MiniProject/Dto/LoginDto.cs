using System.ComponentModel.DataAnnotations;

namespace MiniProject.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
