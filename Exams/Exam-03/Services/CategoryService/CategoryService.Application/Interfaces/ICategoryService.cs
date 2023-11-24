using CategoryService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoryService.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> AddAsync(Category category);

        public Task<IEnumerable<Category>> GetAllAsync();

        public Task<Category> GetByIdAsync(int id);

        public Task UpdateAsync(int id, Category category);

        public Task RemoveAsync(int id);
    }
}