using System.Threading.Tasks;
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
            var original = await _categoryRepository.GetByIdAsync(category.Id);

            if (original != null)
            {
                throw new BadRequestException($"Category with Id={category.Id} Alredy Exists");
            }

            await _categoryRepository.AddAsync(category);

            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Category [{100}] Not Found");
            }

            var products = await _productRepository.FindAsync(p => p.CategoryId == original.Id);

            foreach (var producto in products)
            {
                await _productRepository.RemoveAsync(producto);
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
                throw new NotFoundException($"Category [{100}] Not Found");
            }

            return teamMember!;
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
            }

            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Category [{100}] Not Found");
            }

            await _categoryRepository.UpdateAsync(category);

            return category!;
        }
    }
}
