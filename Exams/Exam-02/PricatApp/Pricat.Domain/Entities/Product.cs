using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities;

public class Product : EntityBase
{
    [Required]
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }
    
    // Propiedad de navegación
    public Category? Category { get; set; }
    
    [Required]
    [StringLength(13)]
    public required string EanCode { get; set; }
    
    [Required]
    [StringLength(50)]
    public required string Description { get; set; }
    
    [Required]
    [StringLength(13)]
    public required string Unit { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(13,2)")]
    public decimal Price { get; set; }
    
}