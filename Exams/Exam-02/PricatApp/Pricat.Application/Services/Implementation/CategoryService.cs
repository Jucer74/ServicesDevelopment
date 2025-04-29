using AutoMapper;
using Pricat.Application.DTO;
using Pricat.Application.Services.Interfaces;
using Pricat.Application.Common.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Application.Exceptions;
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
            if (string.IsNullOrWhiteSpace(categoryCreateDto.Description))
            {
                throw new BadRequestException("El nombre de la categoría no puede estar vacío.");
            }

            var mappedCategory = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(mappedCategory);

            var insertedCategory = await _categoryRepository.GetByIdAsync(mappedCategory.Id);
            if (insertedCategory == null)
            {
                throw new InternalServerErrorException("Error al recuperar la categoría recién creada.");
            }

            return _mapper.Map<CategoryResultDto>(insertedCategory);
        }

        public async Task<IEnumerable<CategoryResultDto>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                throw new NotFoundException("No se encontraron categorías registradas.");
            }

            return _mapper.Map<IEnumerable<CategoryResultDto>>(categories);
        }

        public async Task<CategoryResultDto> GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new BadRequestException("El ID de categoría debe ser mayor que cero.");
            }

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

            return _mapper.Map<CategoryResultDto>(category);
        }

        public async Task<CategoryResultDto> UpdateCategory(int categoryId, CategoryCreateDto categoryUpdateDto)
        {
            if (categoryId <= 0)
            {
                throw new BadRequestException("El ID de categoría no es válido.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new NotFoundException($"La categoría con ID {categoryId} no existe.");
            }

            _mapper.Map(categoryUpdateDto, existingCategory);
            await _categoryRepository.UpdateAsync(existingCategory);

            return _mapper.Map<CategoryResultDto>(existingCategory);
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new BadRequestException("ID de categoría inválido para eliminación.");
            }

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                throw new NotFoundException($"La categoría con ID {categoryId} no fue encontrada para eliminar.");
            }

            var productsToDelete = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
            foreach (var product in productsToDelete)
            {
                await _productRepository.RemoveAsync(product);
            }

            await _categoryRepository.RemoveAsync(existingCategory);
            return true;
        }
    }
}
