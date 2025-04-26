using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
