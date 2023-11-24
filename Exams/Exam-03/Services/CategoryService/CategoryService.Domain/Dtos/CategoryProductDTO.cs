using System;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Domain.Dtos
{
	public class CategoryProductDTO
	{
        public int CategoryId { get; set; }
        public string EanCode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
    }
}

