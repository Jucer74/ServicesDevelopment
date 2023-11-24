using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IProductService
    {
        public Task<EProduct> AddAsync(EProduct product);

        public Task<IEnumerable<EProduct>> GetAllAsync();

        public Task<EProduct> GetByIdAsync(int id);

        public Task<IEnumerable<EProduct>> GetProductsByCategoryId(int categoryId);

        public Task UpdateAsync(int id, EProduct product);

        public Task RemoveAsync(int id);

        public Task RemoveProductsByCategoryId(int categoryId);
    }
}