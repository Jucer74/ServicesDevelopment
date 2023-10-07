<<<<<<< HEAD
using System.Linq.Expressions;
=======
﻿using System.Linq.Expressions;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83

namespace NetBank.Domain.Common
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task<T> AddAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();

<<<<<<< HEAD
        public Task<T?> GetByIdAsync(int id);

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task UpdateAsync(T entity);

        public Task RemoveAsync(T entity);

        public Task RemoveRangeAsync(IEnumerable<T> entityList);
=======
        public Task<T> GetByIdAsync(int id);

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task<T> UpdateAsync(T entity);

        public Task RemoveAsync(T entity);
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
    }
}