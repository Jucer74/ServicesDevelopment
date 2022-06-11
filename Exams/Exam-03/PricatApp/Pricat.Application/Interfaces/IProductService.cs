using Pricat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pricat.Application.Interfaces
{
    public interface IProductService
    {
        public Task AddAsync(Product entity);

        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<Product> GetByIdAsync(int id);

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);

        public Task UpdateAsync(int id, Product entity);

        public Task RemoveAsync(int id);

        public Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId);

        public Task RemoveAllByCategoryIdAsync(int categoryId);

    }
}