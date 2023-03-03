using System.ComponentModel.DataAnnotations;

namespace ShopApp.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Title { get; set; }
        [StringLength(45)]
        public string? ImageUrl { get; set; }
        [Required]
        public double Price { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
