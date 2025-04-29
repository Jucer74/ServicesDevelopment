using System.Linq.Expressions;
using Pricat.Application.Common;
using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;

namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;

    public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<Product> AddAsync(Product entity)
    {
        var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with Id={entity.CategoryId} not found.");
        }

        return await _productRepository.AddAsync(entity);
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }
    
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException($"Product with Id={id} not found.");
        }

        return product;
    }
    
    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with Id={categoryId} not found.");
        }

        // Obtener los productos de la categoría
        return await _productRepository.FindAsync(p => p.CategoryId == categoryId);
    }
    
    public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _productRepository.FindAsync(predicate);
    }
    
    public async Task<Product> UpdateAsync(int id,Product entity)
    {
        var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category with Id={entity.CategoryId} not found.");
        }

        return await _productRepository.UpdateAsync(entity);
    }
    
    public async Task RemoveAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if (product == null)
        {
            throw new NotFoundException($"Product with Id={id} not found.");
        }
        
        await _productRepository.RemoveAsync(product);
    }
}