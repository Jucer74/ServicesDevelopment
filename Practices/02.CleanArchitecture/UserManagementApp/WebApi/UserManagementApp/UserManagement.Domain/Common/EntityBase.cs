using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Common;
public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}
