using Pricat.Domain.Entities;

namespace Pricat.Domain.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
}
