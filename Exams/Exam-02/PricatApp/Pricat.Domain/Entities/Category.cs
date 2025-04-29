using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities;

public class Category : EntityBase
{
    [StringLength(50)]
    [Required]
    public required string Description { get; set; } 
    
    // Relación con productos
    public ICollection<Product> Products { get; set; } = new List<Product>();
}