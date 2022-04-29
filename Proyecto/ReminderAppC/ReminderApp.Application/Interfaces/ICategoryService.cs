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
        public Task AddAsync(Category entity);

        public Task<IEnumerable<Category>> GetAllAsync();

        public Task<Category> GetByIdAsync(int id);

        public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);

        public Task UpdateAsync(int id, Category entity);

        public Task RemoveAsync(int id);
    }
}
