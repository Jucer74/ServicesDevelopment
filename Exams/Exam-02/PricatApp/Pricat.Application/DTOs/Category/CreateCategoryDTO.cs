using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pricat.Application.DTOs.Category
{

    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        [RegularExpression(@"^(?!\d+$)[\p{L}\p{N}\s]+$", ErrorMessage = "Description must not contain only numbers")]
        public required string Description { get; set; }
    }
}