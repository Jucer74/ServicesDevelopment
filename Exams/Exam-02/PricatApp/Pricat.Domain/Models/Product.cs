using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pricat.Domain.Common;
namespace Pricat.Domain.Models;

[Table("products")] 
public class Product: Entitybase
{
    [ForeignKey("CategoryId")]
    [Required]
    public required int CategoryId { get; set; }
    
    public required Category Category { get; set; } 
    
    [StringLength(13)]
    [Required]
    public required string EanCode { get; set; }
    
    [StringLength(50)]
    [Required]
    public required string Description { get; set; }
    
    [StringLength(20)]
    [Required]
    public required string Unit { get; set; } 
    
    [Required]
    [Column(TypeName = "decimal(13,2)")]
    public required decimal Price { get; set; }
    
    
}