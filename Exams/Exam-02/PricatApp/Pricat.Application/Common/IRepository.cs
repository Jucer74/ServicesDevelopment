using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pricat.Domain.Common;

namespace Pricat.Application.Common
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<T> AddAsync(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public Task<T> UpdateAsync(int id, T entity);
        public Task DeleteAsync(T entity);

    }
}