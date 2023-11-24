using System;
using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Dtos
{
	public class ProductDto
	{
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }
    }
}

