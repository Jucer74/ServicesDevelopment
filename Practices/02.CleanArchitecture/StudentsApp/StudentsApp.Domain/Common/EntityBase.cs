using System.ComponentModel.DataAnnotations;

namespace StudentsApp.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}