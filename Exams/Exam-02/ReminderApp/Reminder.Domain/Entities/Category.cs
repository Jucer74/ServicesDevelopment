using ReminderAPP.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderAPP.Domain.Entities
{
    public class Category: EntiyBase
    {

        [Required]
        public string description { get; set; }
    }

}
