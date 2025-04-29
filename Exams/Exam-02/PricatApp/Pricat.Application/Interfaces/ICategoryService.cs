using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddAsync(Category entity);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);
        Task<Category> UpdateAsync(int id, Category entity);
        Task DeleteAsync(int id);
    }
}