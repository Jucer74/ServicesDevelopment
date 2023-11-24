using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Domain.Interfaces.Proxies;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductProxy _productProxy;

        public CategoryService(ICategoryRepository CategoryRepository, IProductProxy productProxy)
        {
            _categoryRepository = CategoryRepository;
            _productProxy = productProxy;
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            return category;
        }

        public async Task RemoveAsync(int id)
        {
            var products = await GetProductsByCategoryIdAsync(id);
            if (products.Any())
            {
                await _productProxy.RemoveRangeAsync(products);
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task UpdateAsync(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
            }

            await _categoryRepository.UpdateAsync(category);
        }

        private async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productProxy.GetProductsByCategoryId(categoryId);
        }
    }
}