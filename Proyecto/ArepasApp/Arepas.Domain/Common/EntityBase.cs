using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      [Required]
      public int Id { get; set; }
   }
}