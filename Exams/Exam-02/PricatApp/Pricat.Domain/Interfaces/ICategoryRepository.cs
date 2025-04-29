using Pricat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pricat.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(int id, Category category);
        Task RemoveAsync(int id);
    }
}