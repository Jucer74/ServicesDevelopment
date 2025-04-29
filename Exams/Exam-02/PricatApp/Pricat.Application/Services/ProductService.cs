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

    public ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    // Crea un nuevo producto validando la categoría y el código EAN
    public async Task<Product> CreateAsync(Product product)
    {
        // Validación de existencia de la categoría
        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        if (category is null)
        {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
        }

        // Validación del código EAN
        if (!Ean13Calculator.IsValid(product.EanCode))
        {
            throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
        }

        // Verificación si existe producto con el mismo código EAN (aunque no se usa después)
        var existingProduct = await _productRepository.FindAsync(p => p.EanCode == product.EanCode);

        return await _productRepository.AddAsync(product);
    }

    // Elimina un producto por su ID
    public async Task DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        await _productRepository.RemoveAsync(product);
    }

    // Retorna todos los productos
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    // Retorna un producto por su ID
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        return product;
    }

    // Obtiene productos por ID de categoría (verifica existencia de la categoría)
    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null)
        {
            throw new NotFoundException($"Category [{categoryId}] Not Found");
        }

        return await _productRepository.GetByCategoryIdAsync(categoryId);
    }

    // Actualiza un producto validando ID, categoría y EAN
    public async Task<Product> UpdateAsync(int id, Product product)
    {
        if (!Ean13Calculator.IsValid(product.EanCode))
        {
            throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
        }

        if (id != product.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
        }

        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct is null)
        {
            throw new NotFoundException($"Product [{id}] Not Found");
        }

        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        if (category is null)
        {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
        }

        return await _productRepository.UpdateAsync(product);
    }

    // Alternativa para obtener productos por categoría (usando FindAsync directamente)
    public async Task<List<Product>> GetProductsByCategory(int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null)
        {
            throw new NotFoundException($"Category [{categoryId}] Not Found");
        }

        return (await _productRepository.FindAsync(p => p.CategoryId == categoryId)).ToList();
    }
}
