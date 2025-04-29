using System.Linq.Expressions;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Categories> AddAsync(Categories entity);
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(int id);
        Task<IEnumerable<Categories>> FindAsync(Expression<Func<Categories, bool>> predicate);
        Task <Categories> UpdateAsync(int id,Categories entity);
        Task DeleteAsync(int id);
    }
}
