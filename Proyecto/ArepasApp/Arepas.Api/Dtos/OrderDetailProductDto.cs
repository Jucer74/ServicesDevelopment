namespace Arepas.Api.Dtos
{
    public class OrderDetailProductDto
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal PriceOrd { get; set; }

        public string? Image { get; set; }
    }
}
