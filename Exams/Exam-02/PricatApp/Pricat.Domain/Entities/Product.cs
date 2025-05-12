using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "EanCode is Required")]
    [StringLength(13, ErrorMessage = "EanCode's Max Length is 13 digits")]
    public string EAN13 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Unit is Required")]
    [StringLength(20, ErrorMessage = "Unit's Max Length is 20 Characters")]
    public string Unit { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }

    public Category? Category { get; set; }
}
