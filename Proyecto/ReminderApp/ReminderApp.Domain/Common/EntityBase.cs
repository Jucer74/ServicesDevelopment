﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Domain.Common
{
     public abstract class EntityBase
    {   
        [Key]
        public int id { get; set; }
    }
}
