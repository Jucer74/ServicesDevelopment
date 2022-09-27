﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeApp.Domain.Common
{
    public interface BadRequestException<T> where T : EntityBase
    {
        public Task<T> AddAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(int id);

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task<T> UpdateAsync(T entity);

        public Task RemoveAsync(T entity);
    }
}