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

        public Task Add(T entity);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        public Task Update(T entity);

        public Task Remove(T entity);

    }
}
