using ReminderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application
{
    public interface IReminderService
    {
        public Task AddAsync(Reminder entity);

        public Task<IEnumerable<Reminder>> GetAllAsync();

    }
}
