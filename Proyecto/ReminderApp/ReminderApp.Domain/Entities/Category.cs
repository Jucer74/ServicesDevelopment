using ReminderApp.Domain.Common;
using System.Collections.Generic;

namespace ReminderApp.Domain.Entities
{
   public class Category: EntityBase
   {
      public Category()
      {
         Reminders = new HashSet<Reminder>();
      }

      // public int Id { get; set; }
      public string Description { get; set; }

      public virtual ICollection<Reminder> Reminders { get; set; }
   }
}