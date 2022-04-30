using ReminderAPP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReminderAPP.Application.Interfaces
{
    public interface IReminderService
    {
        public Task AddAsync(Reminder entity);

        public Task<IEnumerable<Reminder>> GetAllAsync();

        public Task<Reminder> GetByIdAsync(int id);

        public Task<IEnumerable<Reminder>> FindAsync(Expression<Func<Reminder, bool>> predicate);

        public Task UpdateAsync(int id, Reminder entity);

        public Task RemoveAsync(int id);
    }
}
