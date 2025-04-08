using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Common;

public abstract class EntitiyBase
{
    [Key]
    public int Id { get; set; }
}