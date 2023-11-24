using ProductService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Entities;

    public class Product : EntityBase
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = null!;


       [Required]
        public string EanCode { get; set; } = null!;


        [Required]
        public string Description { get; set; } = null!;


        [Required]
        public string Unit { get; set; } = null!;


        [Required]
        public decimal Price { get; set; }
    }

