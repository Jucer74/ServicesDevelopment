using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTOs.Category
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        [RegularExpression(@"^(?!\d+$)[\p{L}\p{N}\s]+$", ErrorMessage = "Description must not contain only numbers")]
        public required string Description { get; set; }
    }
}
