using AutoMapper;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;
using Pricat.Application.Common.Interfaces;
using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricat.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IRepository<Product> productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CategoryResultDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var mappedCategory = _mapper.Map<Category>(categoryCreateDto);

            await _categoryRepository.AddAsync(mappedCategory);

            var insertedCategory = await _categoryRepository.GetByIdAsync(mappedCategory.Id);

            if (insertedCategory == null)
            {
                throw new Exception("Error fetching the created category.");
            }

            return _mapper.Map<CategoryResultDto>(insertedCategory);
        }

        public async Task<IEnumerable<CategoryResultDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResultDto>>(categories);
        }
        public async Task<CategoryResultDto> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new Exception($"Category with ID {categoryId} not found.");
            }

            return _mapper.Map<CategoryResultDto>(category);
        }


        public async Task<CategoryResultDto> UpdateCategory(int categoryId, CategoryCreateDto categoryUpdateDto)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);

            if (existingCategory == null)
            {
                throw new Exception($"Category with ID {categoryId} not found.");
            }

            _mapper.Map(categoryUpdateDto, existingCategory);

            await _categoryRepository.UpdateAsync(existingCategory);

            return _mapper.Map<CategoryResultDto>(existingCategory);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);

            if (existingCategory == null)
            {
                return false;
            }

            // 🔥 Eliminar todos los productos asociados a esta categoría
            var productsToDelete = await _productRepository.FindAsync(p => p.CategoryId == categoryId);

            foreach (var product in productsToDelete)
            {
                await _productRepository.RemoveAsync(product);
            }

            // Ahora eliminamos la categoría
            await _categoryRepository.RemoveAsync(existingCategory);

            return true;
        }
    }
}
