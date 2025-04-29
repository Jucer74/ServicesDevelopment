using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
<<<<<<< HEAD
=======
using UserManagement.Domain.Common;
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Services
{
<<<<<<< HEAD
    internal class UserService : IUserService
    {

        private readonly IUserService _userRepository;

        public UserService(IUserService userRepository)
=======
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
        {
            _userRepository = userRepository;
        }

<<<<<<< HEAD
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
=======
        public Task AddAsync(User entity)
        {
            return _userRepository.AddAsync(entity);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
        }

        public Task<User> GetByIdAsync(int id)
        {
<<<<<<< HEAD
            var user = _userRepository.GetByIdAsync(id);

            if(user == null)
            {
                throw new Exception($"Peroson with Id={id} Not Found");
            }
            return user;
=======
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
}
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
