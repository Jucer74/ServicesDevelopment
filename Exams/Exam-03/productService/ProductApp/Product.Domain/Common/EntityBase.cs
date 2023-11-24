using System.ComponentModel.DataAnnotations;

namespace Product.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}