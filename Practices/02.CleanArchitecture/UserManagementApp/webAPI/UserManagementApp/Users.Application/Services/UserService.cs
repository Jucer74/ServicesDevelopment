using System.Linq.Expressions;
using Users.Application.Exceptions;
using Users.Application.Interfaces;
using Users.Application.Interfaces.Repositories;
using Users.Domain.Entities;

namespace Users.Application.Services
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

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return user;
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            await _userRepository.RemoveAsync(user);
        }

        public async Task<User> UpdateAsync(int id, User entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return (await _userRepository.UpdateAsync(entity));
        }
    }
}