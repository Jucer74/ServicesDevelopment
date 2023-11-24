using TeamsService.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamsServie.Domain.Entities
{
    
    public class Libro : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Titulo { get; set; } = null!;

        [MaxLength(255)]
        public string Imagen { get; set; } = null!;

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(45)]
        public string Categoria { get; set; } = null!;
    }
}
