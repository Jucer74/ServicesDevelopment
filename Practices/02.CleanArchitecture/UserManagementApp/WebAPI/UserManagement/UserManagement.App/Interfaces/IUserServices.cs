using System.Linq.Expressions;
using UserManagement.Dom.Entities;

namespace UserManagement.App.Interfaces;
 
public interface IUserServices
{
    public Task<User> AddAsync(User entity);

    public Task<IEnumerable<User>> GetAllAsync();

    public Task<User> GetByIdAsync(int id);

    public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);

    public Task<User> UpdateAsync( int id, User entity);

    public Task RemoveAsync(int id);
}