using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    public class Product : EntityBase
    {
        [Required]
        public required string EanCode { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Unit { get; set; }

        [Required]
        public required decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public required Category Categoria { get; set; }
    }
}