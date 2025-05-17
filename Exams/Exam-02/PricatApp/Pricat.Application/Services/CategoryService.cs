using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces;
using Pricat.Application.Exceptions;
using System.Linq.Expressions;
using Pricat.Domain.Entities;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> AddAsync(Category entity)
        {
            return await _categoryRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _categoryRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            return category;
        }

        public async Task RemoveAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{entity.Id}]");
            }

            var category = await _categoryRepository.GetByIdAsync(id);

            return (await _categoryRepository.UpdateAsync(entity));
        }
    }
}
