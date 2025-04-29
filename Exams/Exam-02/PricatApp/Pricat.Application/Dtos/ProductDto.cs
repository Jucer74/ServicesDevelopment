namespace Pricat.Application.Dtos;

public class ProductDto
{
    public int CategoryId { get; set; }

    public string EanCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public required decimal Price { get; set; }
}