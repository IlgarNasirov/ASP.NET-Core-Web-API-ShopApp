using System.ComponentModel.DataAnnotations;

namespace ShopApp.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
