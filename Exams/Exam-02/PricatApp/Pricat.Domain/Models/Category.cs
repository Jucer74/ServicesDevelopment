using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Models;

public class Category : EntityBase
{
    [Required]
    [StringLength(50)]
    public string Description { get; set; } = null!;
    public virtual List<Product> Products { get; set; } = null!;
}

