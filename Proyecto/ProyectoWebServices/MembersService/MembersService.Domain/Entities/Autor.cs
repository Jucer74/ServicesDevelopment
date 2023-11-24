using MembersService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MembersService.Domain.Entities
{
    public class Autor : EntityBase
    {
        [Required]
        public int Id { get; set; } // Cambiado a int para reflejar el tipo de dato de la columna autor_id

        [Required]
        public int LibroId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string Apellido { get; set; }

        [MaxLength(45)]
        public string Pais { get; set; }
    }
}
