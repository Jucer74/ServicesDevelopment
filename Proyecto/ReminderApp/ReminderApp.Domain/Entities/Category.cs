using ReminderApp.Domain.Common;
using ReminderApp.Domain.Enumerations;
using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderApp.Domain.Entities
{
    public partial class Category: EntityBase
    {
        public Category()
        {
            Reminders = new HashSet<Reminder>();
        }
        public CategoryType Description { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
