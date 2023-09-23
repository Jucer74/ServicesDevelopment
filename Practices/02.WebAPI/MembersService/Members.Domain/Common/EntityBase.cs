using System.ComponentModel.DataAnnotations;

namespace Members.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}