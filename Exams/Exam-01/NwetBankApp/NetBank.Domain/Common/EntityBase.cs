using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      [Required]
      public int Id { get; set; }
   }
}