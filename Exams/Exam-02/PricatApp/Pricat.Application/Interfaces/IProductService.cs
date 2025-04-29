using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTOs;
using Pricat.Domain.Entities;

namespace Pricat.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task<bool> IsEanCodeUniqueAsync(string eanCode, int? excludedId = null);
        Task<ProductDto> AddAsync(ProductDto productDto);
        Task<ProductDto?> UpdateAsync(int id, ProductDto productDto);
        Task DeleteAsync(int id);
    }

}
