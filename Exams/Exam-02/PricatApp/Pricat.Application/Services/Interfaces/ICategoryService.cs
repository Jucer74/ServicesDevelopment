using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTO;

namespace Pricat.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryResultDto> CreateCategory(CategoryCreateDto categoryCreateDto);
        public Task<IEnumerable<CategoryResultDto>> GetCategories();
        public Task<CategoryResultDto> GetCategoryById(int categoryId);

        public Task<CategoryResultDto> UpdateCategory(int categoryId, CategoryCreateDto categoryUpdateDto);
        public Task<bool> DeleteCategory(int categoryId);
    }
}
