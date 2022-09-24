using System.ComponentModel.DataAnnotations;

namespace Employee.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      public int Id { get; set; }
   }
}