using System.ComponentModel.DataAnnotations;

namespace Pricat.Application.DTOs.Product
{
    public class UpdateProductDTO
    {
        public required string EanCode { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public required string Description { get; set; }

        [Required]
        public required string Unit { get; set; }

        public decimal Price { get; set; }

        public int CategoriaId { get; set; }


    }
}
