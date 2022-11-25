using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
