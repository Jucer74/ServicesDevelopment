using System.ComponentModel.DataAnnotations;

namespace Students.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}