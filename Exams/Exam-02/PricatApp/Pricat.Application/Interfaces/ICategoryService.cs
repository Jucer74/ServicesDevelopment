using Pricat.Application.DTOs;

namespace Pricat.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto?> GetByIdAsync(int id);

        Task<CategoryDto> AddAsync(CategoryDto categoryDto);

        Task<CategoryDto?> UpdateAsync(int id, CategoryDto categoryDto);

        Task DeleteAsync(int id);
    }
}