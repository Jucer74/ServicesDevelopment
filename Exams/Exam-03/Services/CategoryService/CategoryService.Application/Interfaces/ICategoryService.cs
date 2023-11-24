using CategoryService.Domain.Dtos;
using CategoryService.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoryService.Application.Interfaces
{
    public interface ICategoryService
    {
        

        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(int id, Category category);
        Task RemoveCategory(int id);
       

        Task<IEnumerable<CategoryProductDto>> GetProductByCategoryId(int id);
        


    }
}