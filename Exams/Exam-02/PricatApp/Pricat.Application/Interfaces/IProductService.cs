using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTOs;
using Pricat.Domain.Common;


namespace Pricat.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(int categoryId);
        Task<ProductDto> AddProductAsync(ProductCreateDto dto);
        Task UpdateProductAsync(ProductDto dto);
        Task DeleteProductAsync(int id);
    }
}
