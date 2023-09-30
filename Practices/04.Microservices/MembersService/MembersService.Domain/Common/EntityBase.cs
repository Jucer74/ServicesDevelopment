using System.ComponentModel.DataAnnotations;

namespace MembersService.Domain.Common;

public abstract class EntityBase
{
    [Key]
    public int Id { get; set; }
}