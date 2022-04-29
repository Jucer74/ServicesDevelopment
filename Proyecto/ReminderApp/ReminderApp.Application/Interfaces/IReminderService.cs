using ReminderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application.Interfaces
{
    public interface IReminderService
    {
        public Task Add(Reminder entity);

        public Task<IEnumerable<Reminder>> GetAll();

        public Task<Reminder> GetById(int id);

        public Task<IEnumerable<Reminder>> Find(Expression<Func<Reminder, bool>> predicate);

        public Task Update (int id, Reminder entity);

        public Task Remove(int id);
        public Task DeleteAllByCategoryId(int id);

        public Task<IEnumerable<Reminder>> GetAllBycategoryId(int id);
    }
}
