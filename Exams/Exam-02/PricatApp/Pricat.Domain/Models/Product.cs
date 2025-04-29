using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Pricat.Domain.Common;

namespace Pricat.Domain.Models;

public class Product : EntityBase
{
    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    [Required]
    public string EanCode { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public string Unit { get; set; } = null!;
    [Required]
    public required decimal Price { get; set; }

    public Category? Category { get; set; }
}