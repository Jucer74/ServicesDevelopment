using System.Linq.Expressions;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;

namespace UserManagement.Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
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

    public Task UpdateAsync(User entity)
    {
        return _userRepository.UpdateAsync(entity);
    }

    public Task RemoveAsync(int id)
    {
        return _userRepository.RemoveAsync(id);
    }

}