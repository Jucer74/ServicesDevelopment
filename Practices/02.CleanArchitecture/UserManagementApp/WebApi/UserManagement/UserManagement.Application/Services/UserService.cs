using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task AddAsync(User entity)
        {
            return _userRepository.AddAsync(entity);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.FindAsync(predicate);
        }

        public Task UpdateAsync(int id, User entity)
        {
            return _userRepository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.RemoveAsync(user);
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}