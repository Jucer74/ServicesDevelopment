using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }
    }
}