using System.Linq.Expressions;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces
{
    public interface IUserService
    {
         Task<User> AddAsync(User entity);

         Task<IEnumerable<User>> GetAllAsync();

         Task<User> GetByIdAsync(int id);

         Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);

         Task<User> UpdateAsync(int id, User entity);

         Task RemoveAsync(int id);
    }
}