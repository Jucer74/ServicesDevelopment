using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      [Required]
      public int Id { get; set; }
   }
}