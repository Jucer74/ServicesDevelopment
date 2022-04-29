using ReminderApp.Domain.Common;
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

        public string Description { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
