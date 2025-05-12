using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Description is Required")]
    [MaxLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
    public string Description { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
