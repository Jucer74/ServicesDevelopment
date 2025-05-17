using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTOs.Category
{
    public class CategoryDTO
    {
     
        public int Id { get; set; }

        [Required]
        public required string Description { get; set; }
    }
}
