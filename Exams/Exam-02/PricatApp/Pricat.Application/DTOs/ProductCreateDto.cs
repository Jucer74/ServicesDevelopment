using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Pricat.Application.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "El ID de la categoría es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser una categoría válida.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El código EAN es obligatorio.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El código EAN debe tener exactamente 13 dígitos.")]
        public string EanCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "La unidad es obligatoria.")]
        [MaxLength(10, ErrorMessage = "La unidad no puede superar los 10 caracteres.")]
        public string Unit { get; set; } = string.Empty;

        [Range(0.01, 1000000, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Price { get; set; }
    }
}


