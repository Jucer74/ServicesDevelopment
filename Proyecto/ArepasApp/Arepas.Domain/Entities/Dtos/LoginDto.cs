using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arepas.Domain.Entities.Dto
{
    public class LoginDto
    {

        [Required(ErrorMessage = "El Email es requerido")]
        [StringLength(255, ErrorMessage = "La longitud maxima es de 255 caracteres")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}