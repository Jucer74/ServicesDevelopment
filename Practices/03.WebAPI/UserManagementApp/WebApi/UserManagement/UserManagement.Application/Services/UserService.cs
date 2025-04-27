using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    internal class UserService : IUserService
    {

        private readonly IUserService _userRepository;

        public UserService(IUserService userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _userRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                throw new Exception($"Peroson with Id={id} Not Found");
            }
            return user;
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"Peroson with Id={id} Not Found");
            }
            await _userRepository.RemoveAsync(id);
        }

        public async Task<User> UpdateAsync(int id,User entity)
        {
            if (id != entity.Id)
            {
                throw new Exception($"The Id={entity.Id} not Found");
            }
            var user = await _userRepository.GetByIdAsync(entity.Id);
            if (user is null)
            {
                throw new Exception($"Person with Id={id} Not found");
            }
            return (await _userRepository.UpdateAsync(id,entity));
        }
    }
}
