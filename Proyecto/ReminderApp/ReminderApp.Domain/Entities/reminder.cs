using System;
using ReminderApp.Domain.Common;
using ReminderApp.Domain.Entities;
namespace ReminderApp.Domain
{
   public class Reminder : EntityBase
    {
        public int categoryid { get; set; }
        public string Descripcion { get; set; }
        public DateTime StartDate { get; set; }
        public string cronEpression { get; set; }
        public int NumberOfTimies { get; set; }
        public bool Enabled { get; set; }
        public virtual Category Category { get; set; }


    }
}
