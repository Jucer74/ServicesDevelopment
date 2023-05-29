namespace Arepas.Api.Dtos;

public class OrderDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string DeliveryFullName { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public string DeliveryPhoneNumber { get; set; } = null!;

    public decimal TotalPrice { get; set; }

    public string? Notes { get; set; }
}