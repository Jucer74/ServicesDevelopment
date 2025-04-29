namespace Pricat.Application.Dtos;

    public class ProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string EanCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

