using System.Linq.Expressions;
using UserManagement.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> UpdateAsync(T entity);
    Task RemoveAsync(T entity);
}
