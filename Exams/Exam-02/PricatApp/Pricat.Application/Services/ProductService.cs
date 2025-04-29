using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Product> CreateProduct(Product product)
    {
        return await _productRepository.AddAsync(product);
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        await _productRepository.RemoveAsync(product);
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return (await _productRepository.GetAllAsync()).ToList();
    }

    public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
    {
        var categoryExists = await _categoryRepository.GetByIdAsync(categoryId);

        if (categoryExists is null)
        {
            throw new NotFoundException($"Category [{categoryId}] Not Found");
        }

        var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);
        return products.ToList();

    }

    public async Task<Product> GetProductById(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        return product;
    }

    public async Task<Product> UpdateProduct(int id, Product entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
        }

        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }
        return await _productRepository.UpdateAsync(entity);
    }
}