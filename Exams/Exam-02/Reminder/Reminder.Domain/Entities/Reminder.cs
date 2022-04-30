using ReminderAPP.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReminderAPP.Domain.Entities
{
    public class Reminder : EntiyBase
    {


        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Description { get; set; }


        public DateTime StartDate { get; set; }

        [Required]
        public string CronExpression { get; set; }

        [Required]
        public int NumberOfTimes { get; set; }

        [Required]
        public bool Enabled { get; set; }
    }
}
