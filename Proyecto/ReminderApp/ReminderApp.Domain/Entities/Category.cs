using ReminderApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Domain.Entities
{
    public class Category: EntityBase
    {
        public Category()
        {
            Reminders = new HashSet<Reminder>();
        }

        public string Descripcion { get; set; }
        public virtual ICollection<Reminder>Reminders { get; set; }
    }
}
