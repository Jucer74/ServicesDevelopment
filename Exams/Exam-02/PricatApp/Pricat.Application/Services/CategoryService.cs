using Pricat.Application.Exceptions;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Application.Interfaces.Services;
using Pricat.Domain.Models;

namespace Pricat.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    // Crea una nueva categoría
    public async Task<Category> CreateAsync(Category category)
    {
        return await _categoryRepository.AddAsync(category);
    }

    // Elimina una categoría y todos los productos asociados a ella
    public async Task DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        // Se obtienen todos los productos y se filtran los que pertenecen a la categoría
        var products = await _productRepository.GetAllAsync();
        var productsToDelete = products.Where(p => p.CategoryId == id).ToList();

        // Se eliminan los productos asociados
        foreach (var product in productsToDelete)
        {
            await _productRepository.RemoveAsync(product);
        }

        // Finalmente, se elimina la categoría
        await _categoryRepository.RemoveAsync(category);
    }

    // Retorna todas las categorías existentes
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    // Retorna una categoría por su ID
    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
            throw new NotFoundException($"Category [{id}] Not Found");

        return category;
    }

    // Actualiza una categoría existente
    public async Task<Category> UpdateAsync(int id, Category entity)
    {
        // Validación de consistencia entre el id del parámetro y el id del objeto
        if (id != entity.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Category.Id [{entity.Id}]");
        }

        var category = await _categoryRepository.GetByIdAsync(id);
        if (category is null)
        {
            throw new NotFoundException($"Category [{id}] Not Found");
        }

        return await _categoryRepository.UpdateAsync(entity);
    }
}
