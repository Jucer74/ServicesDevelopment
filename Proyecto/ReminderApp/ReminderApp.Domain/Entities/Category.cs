using System;
using System.Collections.Generic;

#nullable disable

namespace ReminderApp.Domain.Entities
{
    public partial class Category
    {
        public Category()
        {
            Reminders = new HashSet<Reminder>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
