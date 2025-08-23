using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Application.DTO;

namespace Pricat.Application.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ProductResultDto> CreateProduct(ProductCreateDto productCreateDto);
        public Task<IEnumerable<ProductResultDto>> GetProducts();
        public Task<ProductResultDto> GetProductById(int productId);
        public Task<IEnumerable<ProductResultDto>> GetByCategoryId(int categoryId);
        public Task<ProductResultDto> UpdateProduct(int productId, ProductCreateDto productUpdateDto);
        public Task<bool> DeleteProduct(int productId);
    }
}
