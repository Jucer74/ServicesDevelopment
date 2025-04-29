using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    public class Products : EntityBase
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "EanCode is Required")]
        [StringLength(13, ErrorMessage = "EanCode's Max Length is 13 digits")]
        public string EanCode { get; set; } = null!;

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Unit is Required")]
        [StringLength(20, ErrorMessage = "Unit's Max Length is 20 Characters")]
        public string Unit { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}