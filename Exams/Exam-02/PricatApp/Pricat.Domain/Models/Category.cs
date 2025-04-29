using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pricat.Domain.Common;


namespace Pricat.Domain.Models;

[Table("categories")] 
public class Category : Entitybase
{
    [Required]
    [StringLength(50)]
    
    public required string Description { get; set; }

    public required ICollection<Product> Products { get; set; } = null!;
}