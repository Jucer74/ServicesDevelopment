using System.ComponentModel.DataAnnotations;

namespace UserManagement.Dom.Common;

public abstract class EntityBase
{
    [Key] 
    public int Id { get; set; }
            
}
