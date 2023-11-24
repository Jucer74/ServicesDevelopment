using System;
namespace ProductService.Domain.Dtos
{
	public class ProductPutDto
	{
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string Unit { get; set; }

        public decimal Price { get; set; }

        public string EanCode { get; set; }
    }
}

