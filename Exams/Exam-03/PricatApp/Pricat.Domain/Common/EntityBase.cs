using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}