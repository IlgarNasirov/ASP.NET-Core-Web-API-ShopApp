using System.ComponentModel.DataAnnotations;

namespace ShopApp.DTOs
{
    public class ProductDTO
    {
        [Required]
        [StringLength(30)]
        [MinLength(6)]
        public string Title { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
    }
}
