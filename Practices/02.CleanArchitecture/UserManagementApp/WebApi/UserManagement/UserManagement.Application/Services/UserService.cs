using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
           return await _userRepository.AddAsync(entity);
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.FindAsync(predicate);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new Exception("User not found");
            } 

            return
        }

        public async Task RemoveAsync(int id)
        {
            var user = _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            await _userRepository.RemoveAsync(user);
        }

        public Task UpdateAsync(int id, User entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var user = _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new DirectoryNotFoundException($"User with Id={id} Not Found");
            }

            return _userRepository.UpdateAsync(entity);
        }
    }
}
