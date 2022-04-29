using System.ComponentModel.DataAnnotations;

namespace ReminderApp.Domain.Common
{
   public abstract class EntityBase
   {
      [Key]
      public int Id { get; set; }
   }
}