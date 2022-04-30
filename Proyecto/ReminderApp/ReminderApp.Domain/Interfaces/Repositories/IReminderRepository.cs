﻿using ReminderApp.Domain.Common;
using ReminderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Domain.Interfaces.Repositories
{
    public interface IReminderRepository: IRepository<Reminder>
    {
        public Task<IEnumerable<Reminder>> FindReminderCategory(int id);
    }
    
}
