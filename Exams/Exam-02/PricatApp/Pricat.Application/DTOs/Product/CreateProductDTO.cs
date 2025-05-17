using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTOs.Product
{
    public class CreateProductDTO
    {
        [Required]
        public required string EanCode { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public required string Description { get; set; }
        [Required]
        public required string Unit { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

    }
}
