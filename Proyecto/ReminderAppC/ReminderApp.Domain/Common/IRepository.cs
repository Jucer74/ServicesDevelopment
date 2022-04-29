using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions; 

namespace ReminderApp.Domain.Common
{
    public interface IRepository<T> where T : EntityBase
    {

        public Task AddAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task UpdateAsync(T entity);

        public Task RemoveAsync(T entity);
    }
}
