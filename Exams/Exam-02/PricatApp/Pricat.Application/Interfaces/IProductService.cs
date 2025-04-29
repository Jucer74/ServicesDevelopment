using System.Collections.Generic;
using System.Threading.Tasks;
using Pricat.Application.DTOs;

namespace Pricat.Application.Interfaces;

public interface IProductService
{
    Task<List<ProductDto>> GetAllProducts();
    Task<ProductDto?> GetProductById(int id);
    Task<List<ProductDto>> GetByCategoryId(int categoryId);
    Task<ProductDto> CreateProduct(ProductDto dto);
    Task UpdateProduct(int id, ProductDto dto);
    Task DeleteProduct(int id);
}
