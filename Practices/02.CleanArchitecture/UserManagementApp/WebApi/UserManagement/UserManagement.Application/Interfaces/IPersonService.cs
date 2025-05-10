using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<Person> GetByIdAsync(int id);
    Task<Person> AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(int id);
}