using ReminderApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task Add(Category entity);

        public Task<IEnumerable<Category>> GetAll();

        public Task<Category> GetById(int id);

        public Task<IEnumerable<Category>> Find(Expression<Func<Category, bool>> predicate);

        public Task Update(Category entity);

        public Task Remove(int id);
    }
}
