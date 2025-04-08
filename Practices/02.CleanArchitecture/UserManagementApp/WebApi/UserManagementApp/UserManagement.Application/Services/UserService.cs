1using System.Linq.Expressions;
using People.Domain.Entities;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _UserRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await _UserRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _UserRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var User = await _UserRepository.GetByIdAsync(id);

            if (User is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            return User;
        }

        public async Task RemoveAsync(int id)
        {
            var User = await _UserRepository.GetByIdAsync(id);

            if (User is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            await _UserRepository.RemoveAsync(User);
        }

        public async Task<User> UpdateAsync(int id, User entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var User = await _UserRepository.GetByIdAsync(id);

            if (User is null)
            {
                throw new NotFoundException($"User with Id={id} Not Found");
            }

            return (await _UserRepository.UpdateAsync(entity));
        }
    }
}
}
