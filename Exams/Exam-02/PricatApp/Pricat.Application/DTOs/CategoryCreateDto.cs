using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pricat.Application.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La descripción no puede superar los 100 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}
