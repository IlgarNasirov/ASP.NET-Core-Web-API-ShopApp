using System.ComponentModel.DataAnnotations;

namespace ShopApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(65)]
        public string PasswordHash { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
