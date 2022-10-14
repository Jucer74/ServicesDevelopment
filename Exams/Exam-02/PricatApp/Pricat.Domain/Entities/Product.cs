using System.ComponentModel.DataAnnotations;
using PricatApp.Domain.Common;

namespace PricatApp.Domain.Entities
{
    public class Product : EntityBase
    {
        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public string? EanCode { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Unit { get; set; }

        [Required]
        public double? Price { get; set; }
    }
}