using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Application.DTOs;

namespace Pricat.Application.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
}
