namespace Pricat.Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string EanCode { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Unit { get; set; } = String.Empty;
    public decimal Price { get; set; } 
}