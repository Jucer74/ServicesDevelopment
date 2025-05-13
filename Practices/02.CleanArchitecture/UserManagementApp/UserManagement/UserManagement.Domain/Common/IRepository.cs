using System.Linq.Expressions;

namespace UserManagement.Domain.Common
{
    public interface IRepository<T> where T : EntityBase
    {
        public Task AddAsync(T entity);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(int id);

        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        public Task RemoveAsync(int id, T entity);

        public Task<T> UpdateAsync(T entity);
        
    }
}