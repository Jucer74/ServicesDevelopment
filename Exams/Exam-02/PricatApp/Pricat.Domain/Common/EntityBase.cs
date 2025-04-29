using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Common;

    public class EntityBase
{
    [Key]
    public int Id { get; set; }
}
