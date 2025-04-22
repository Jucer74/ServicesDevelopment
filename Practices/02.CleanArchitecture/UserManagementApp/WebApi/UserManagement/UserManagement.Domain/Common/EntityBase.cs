using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Common;

public class EntityBase
{
    [Key]
    public int Id { get; set; }

}
