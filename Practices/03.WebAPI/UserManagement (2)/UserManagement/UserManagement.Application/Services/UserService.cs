using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Exceptions;
using UserManagement.Domain.Interfaces.Repositories;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            await _userRepository.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"User with Id={id} not found");
            }

            return user;
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _userRepository.FindAsync(predicate);
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"User with Id={id} not found");
            }

            await _userRepository.RemoveAsync(id, user);

        }

        public async Task<User> UpdateAsync(int id, User entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} does not match Entity.Id={entity.Id}");
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"User with Id={id} not found");
            }

            return await _userRepository.UpdateAsync(entity);
        }
    }
}
