

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pricat.Domain.Common;

namespace Pricat.Domain.Models
{
    public class Product : EntityBase
    {
        [ForeignKey("CategoryId")]
        [Required]
        public required int CategoryId { get; set; }

        [StringLength(13)]
        [Required]
        public required string EanCode { get; set; }

        [StringLength(50)]
        [Required]
        public required string Description { get; set; }

        [StringLength(20)]
        [Required]
        public required string Unit { get; set; }

        [Required]
        public required double Price { get; set; }
        
        public Category? Category { get; set; }
    }
}
