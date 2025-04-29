using System.Linq.Expressions;
using Pricat.Application.Common;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;

namespace Pricat.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Product> _productRepository;

    public CategoryService(IRepository<Category> categoryRepository, IRepository<Product> productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }
    
    public async Task<Category> AddAsync(Category entity)
    {
        return await _categoryRepository.AddAsync(entity);
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        // Validar si el ID es inválido
        if (!int.TryParse(id.ToString(), out var numericId) || numericId <= 0)
        {
            throw new BadRequestException($"The value '{numericId}' is not valid.");
        }
        
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        return category;
    }
    
    public async Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _categoryRepository.FindAsync(predicate);
    }

    public async Task<Category> UpdateAsync(int id, Category entity)
    {
        return await _categoryRepository.UpdateAsync(entity);
    }
    
    public async Task RemoveAsync(int id)
    {
        // Verificar si la categoría existe
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        // Verificar si hay productos asociados
        var products = await _productRepository.FindAsync(p => p.CategoryId == id);
        if (products.Any())
        {
            // Eliminar los productos asociados
            foreach (var product in products)
            {
                await _productRepository.RemoveAsync(product);
            }
        }

        // Eliminar la categoría (los productos se eliminan automáticamente)
        await _categoryRepository.RemoveAsync(category);
    }

}