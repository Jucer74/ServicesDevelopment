using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int id)
        {

            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            var products = await _productRepository.GetAllAsync();
            var productsToDelete = products.Where(p => p.CategoryId == id).ToList();

            foreach (var product in productsToDelete)
            {
                await _productRepository.RemoveAsync(product);
            }

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return (await _categoryRepository.GetAllAsync()).ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            return category;
        }

        public async Task<Category> UpdateCategory(int id, Category entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{entity.Id}]");
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }
            return await _categoryRepository.UpdateAsync(entity);
        }
    }
}
