using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using Pricat.Application.Exceptions; // Verás en el siguiente paso que creamos estas excepciones

namespace Pricat.Application.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _repository;

        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category [{id}] Not Found");
            return category;
        }

        public async Task<Category> CreateAsync(Category category)
            => await _repository.AddAsync(category);

        public async Task UpdateAsync(Category category)
        {
            if (!await _repository.ExistsAsync(c => c.Id == category.Id))
                throw new NotFoundException($"Category [{category.Id}] Not Found");
            await _repository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Category [{id}] Not Found");
            await _repository.DeleteAsync(entity);
        }
    }
}
