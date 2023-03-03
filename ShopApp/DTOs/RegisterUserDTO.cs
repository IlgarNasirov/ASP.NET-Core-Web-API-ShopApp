using System.ComponentModel.DataAnnotations;

namespace ShopApp.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(30)]
        [MinLength(6)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(65)]
        [MinLength(6)]
        public string Password { get; set; }
        [Compare("Password")]
        public string Repassword { get; set; }
    }
}
