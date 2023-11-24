using System.ComponentModel.DataAnnotations;

namespace ProductService.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      
      public int Id { get; set; }
   }
}