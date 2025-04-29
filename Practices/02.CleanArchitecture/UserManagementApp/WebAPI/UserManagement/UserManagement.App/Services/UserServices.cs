using System.Linq.Expressions;
using UserManagement.App.Exceptions;
using UserManagement.App.Interfaces;
using UserManagement.Dom.Entities;

using UserManagement.Dom.Interfaces.Repositories;


namespace UserManagement.App.Services;

public class UserServices: IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
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
            throw new NotFoundException($"User with Id={id} Not Found");
        }
         
        return user;
    }

    public async Task RemoveAsync(int id)
    {
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

        var person = await _userRepository.GetByIdAsync(id);

        if (person is null)
        {
            throw new NotFoundException($"Person with Id={id} Not Found");
        }

        return (await _userRepository.UpdateAsync(entity));
    }
    
}