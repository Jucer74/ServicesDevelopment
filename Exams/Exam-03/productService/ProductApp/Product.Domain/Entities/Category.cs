using Product.Domain.Common;

namespace Product.Domain.Entities;

public class Category:EntityBase
{
    public string Description { get; set; } = null!;
}