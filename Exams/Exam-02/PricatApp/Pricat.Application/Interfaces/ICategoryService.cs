using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pricat.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Application.DTOs;

namespace Pricat.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task<CategoryDto> AddCategoryAsync(CategoryCreateDto dto);
        Task UpdateCategoryAsync(CategoryDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
