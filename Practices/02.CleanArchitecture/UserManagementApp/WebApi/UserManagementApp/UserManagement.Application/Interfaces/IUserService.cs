using System.Linq.Expressions;
using People.Domain.Entities;

namespace UserManagement.Application.Interfaces;
public interface IUserService 
{
    public Task<User> AddAsync(User entity);
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User> GetByIdAsync(int id);
    public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);
    public Task<User> UpdateAsync(int id, User entity);
    public Task RemoveAsync(int id);
}
