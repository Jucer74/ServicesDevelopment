using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Services;
public interface ICategoryService
{
    Task<Category> CreateAsync(Category category);
    Task DeleteAsync(int id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task<Category> UpdateAsync(int id, Category category);
}
