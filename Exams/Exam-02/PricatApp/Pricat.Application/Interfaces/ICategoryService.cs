using System.Linq.Expressions;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces;

public interface ICategoryService
{
    public Task<Category> AddAsync(Category entity);
    public Task<IEnumerable<Category>> GetAllAsync();
    public Task<Category> GetByIdAsync(int id);
    public Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate); 
    public Task<Category> UpdateAsync(int id, Category entity);
    public Task RemoveAsync(int id);
}