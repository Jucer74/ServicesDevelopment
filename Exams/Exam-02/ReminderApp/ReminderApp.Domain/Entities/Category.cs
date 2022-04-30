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
         
        }
        public CategoryType Description { get; set; }

     
    }
}
