using System.Linq.Expressions;

namespace UserManagement.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
    public Task<T> AddAsync(T entity);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<T> GetByIdAsync(int id);
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    public Task<T> UpdateAsync(T entity);
=======
>>>>>>> 219361b297b922b7a9e1dd565c70121e55f718f4
    public Task AddAsync(T entity);

    public Task<IEnumerable<T>> GetAllAsync();

    public Task<T> GetByIdAsync(int id);

    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    public Task UpdateAsync(T entity);

<<<<<<< HEAD
=======
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
>>>>>>> 219361b297b922b7a9e1dd565c70121e55f718f4
    public Task RemoveAsync(T entity);
}