using System.Linq.Expressions;
using Users.Domain.Entities;

namespace Users.Application.Interfaces
{
    public interface IUserService
    {
        public Task<User> AddAsync(User entity);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);
        public Task<User> UpdateAsync(int Id, User entity);
        public Task RemoveAsync(int id);
    }
}
