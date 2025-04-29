using System.ComponentModel.DataAnnotations;

namespace Pricat.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "EAN Code is required")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "EAN Code must be 13 characters")]
        public string EanCode { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Unit is required")]
        [StringLength(10, ErrorMessage = "Unit cannot exceed 10 characters")]
        public string Unit { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
}