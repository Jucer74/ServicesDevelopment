using System.ComponentModel.DataAnnotations;

namespace Users.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
