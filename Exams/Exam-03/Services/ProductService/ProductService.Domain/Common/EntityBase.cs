using System.ComponentModel.DataAnnotations;

namespace ProductServiceAPI.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      [Required]
      public int Id { get; set; }
   }
}