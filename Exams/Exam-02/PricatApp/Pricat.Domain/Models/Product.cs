

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Models;

public class Product : EntityBase 
{
    [Required]
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(13)]
    public string EanCode { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string Unit { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(13,2)")]
    public decimal Price { get; set; }

    public virtual Category Category { get; set; } = null!;
}
