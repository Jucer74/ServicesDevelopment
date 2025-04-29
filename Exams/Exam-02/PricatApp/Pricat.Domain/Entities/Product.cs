using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(13)]
        public string EanCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        // Propiedad de navegación (opcional, para la relación con Category)
        public Category Category { get; set; } = null!;
    }
}
