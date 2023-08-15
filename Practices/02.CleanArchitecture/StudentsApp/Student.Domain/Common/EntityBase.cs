
using System.ComponentModel.DataAnnotations;

namespace Student.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
