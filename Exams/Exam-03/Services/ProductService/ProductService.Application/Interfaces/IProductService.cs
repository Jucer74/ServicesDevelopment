using ProductService.Application.dto;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Product> AddAsync(ProductDto product);

        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetByIdAsync(int id);

        public Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);

        public Task UpdateAsync(int id, ProductDto product);

        public Task RemoveAsync(int id);
    }
}