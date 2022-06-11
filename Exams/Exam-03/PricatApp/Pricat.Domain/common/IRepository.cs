using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Domain.common
{
    public interface IRepository<T> where T : Entitybase
    {

        public Task Add(T entity);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int id);

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        public Task Update(T entity);

        public Task Remove(T entity);

    }
}
