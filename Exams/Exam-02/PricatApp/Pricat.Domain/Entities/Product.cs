using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Entities; // si es la ruta correcta


namespace Pricat.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "EanCode is Required")]
        [StringLength(13, ErrorMessage = "EanCode's Max Length is 13 digits")]
        public string EanCode { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Unit is Required")]
        [MaxLength(20, ErrorMessage = "Unit's Max Length is 20 Characters")]
        public string Unit { get; set; } = null!;

        public decimal Price { get; set; }

        // Propiedad de navegación
        public Category Category { get; set; } = null!;
    }

}
