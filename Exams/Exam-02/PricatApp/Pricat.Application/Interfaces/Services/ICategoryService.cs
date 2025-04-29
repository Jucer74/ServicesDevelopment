using Pricat.Domain.Models;
namespace Pricat.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<Category> CreateCategory(Category category);

    Task<List<Category>> GetAllCategories();

    Task<Category> GetCategoryById(int id);

    Task<Category> UpdateCategory(int id, Category category);

    Task<List<Product>> GetProductsByCategoryId(int id);
    Task DeleteCategory(int id);
}