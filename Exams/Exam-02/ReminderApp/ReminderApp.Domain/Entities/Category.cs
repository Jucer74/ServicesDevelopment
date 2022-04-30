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
        }

        public string Description { get; set; }


    }
}
