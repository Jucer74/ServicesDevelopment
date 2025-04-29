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
            var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
            if (existingCategory != null)
            {
                throw new BadRequestException($"Category with Id [{category.Id}] already exists.");
            }
            await _categoryRepository.AddAsync(category);

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var original = await _categoryRepository.GetByIdAsync(id);
            

            if (original is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }
            
            var products = await _productRepository.FindAsync(x => x.CategoryId == id);
            foreach (var product in products)
            {
                await _productRepository.RemoveAsync(product);
            }

            await _categoryRepository.RemoveAsync(original);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return (await _categoryRepository.GetAllAsync()).ToList();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            return category;
        }

        public async Task<Category> CategoryUpdate(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
            }

            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            await _categoryRepository.UpdateAsync(category);

            return category;
        }
    }
}