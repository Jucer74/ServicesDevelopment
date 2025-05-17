using System.Linq.Expressions;
<<<<<<< HEAD
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Interfaces.Repositories;
=======
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Common;
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
    public class UserService : IUserService
    {
<<<<<<< HEAD
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository UserRepository)
        {
            _userRepository = UserRepository;
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
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            return user;
=======
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
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
        }

        public async Task RemoveAsync(int id)
        {
<<<<<<< HEAD
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
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
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            return (await _userRepository.UpdateAsync(entity));
        }
    }
=======
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

    {
    }
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
}