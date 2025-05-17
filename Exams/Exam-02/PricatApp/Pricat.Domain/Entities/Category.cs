using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    public class Category : EntityBase
    {
        [Required]
        public required string Description { get; set; }

        public required ICollection<Product> Productos { get; set; }
    }
}