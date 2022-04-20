using ReminderApp.Domain.Common;
using System;

namespace ReminderApp.Domain.Entities
{
   public class Reminder : EntityBase
   {
      public int CategoryId { get; set; }

      public string Description { get; set; }
      public DateTime StartDate { get; set; }
      public string CronExpression { get; set; }
      public int? NumberOfTimes { get; set; }
      public bool? Enabled { get; set; }

      public virtual Category Category { get; set; }
   }
}