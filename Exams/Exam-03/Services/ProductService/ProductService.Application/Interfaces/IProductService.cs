using ProductServiceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductServiceAPI.Application.Interfaces
{
   public interface IProductService
   {
      public Task<Product> AddAsync(Product product);

      public Task<IEnumerable<Product>> GetAllAsync();

      public Task<Product> GetByIdAsync(int id);

      public Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);

      public Task UpdateAsync(int id, Product product);

      public Task RemoveAsync(int id);

      public Task RemoveByCategoryIdAsync(int categoryId);
   }
}