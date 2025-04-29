using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;
using Pricat.Utilities;
namespace Pricat.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }
    public async Task<Product> CreateProduct(Product product)
    {

        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        var original = await _productRepository.GetByIdAsync(product.Id);
        if (original != null)
        {
            throw new BadRequestException($"Product [{product.Id}] Not Found");
        }


        if (category is null)
        {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
        }

        if (!Ean13Calculator.IsValid(product.EanCode))
        {
            throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
        }


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
        var category = await _categoryRepository.GetByIdAsync(entity.CategoryId);
        var product = await _productRepository.GetByIdAsync(id);

        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{entity.Id}]");
        }

        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        if (category is null)
        {
            throw new NotFoundException($"Category [{entity.CategoryId}] Not Found");
        }

        if (!Ean13Calculator.IsValid(entity.EanCode))
        {
            throw new BadRequestException($"EAN Code [{entity.EanCode}] is Not Valid");
        }

        return await _productRepository.UpdateAsync(entity);
    }
}