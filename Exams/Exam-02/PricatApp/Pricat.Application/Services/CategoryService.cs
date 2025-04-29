using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _categoryRepository.AddAsync(category);

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Category with Id={id} Not Found");
            }

            await _categoryRepository.RemoveAsync(original);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return (await _categoryRepository.GetAllAsync()).ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var teamMember = await _categoryRepository.GetByIdAsync(id);
            if (teamMember is null)
            {
                throw new NotFoundException($"Category with Id={id} Not Found");
            }

            return teamMember!;
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{category.Id}]");
            }

            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Team Member with Id={id} Not Found");
            }

            await _categoryRepository.UpdateAsync(category);

            return category!;
        }
    }
}
