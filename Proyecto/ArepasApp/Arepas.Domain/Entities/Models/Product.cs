using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Entities.Models
{
    public class Product : EntityBase
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public string? ImageName { get; set; }

    }
}
