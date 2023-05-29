namespace Arepas.Api.Dtos;

public class OrderDetailDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceOrd { get; set; }
}