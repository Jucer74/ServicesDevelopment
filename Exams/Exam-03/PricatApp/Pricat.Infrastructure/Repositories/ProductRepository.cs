using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository 
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await FindAsync(p => p.CategoryId == categoryId);
        }

        public async Task RemoveAllByCategoryIdAsync(int categoryId)
        {
            var productsToRemove = await GetAllByCategoryIdAsync(categoryId);
            await RemoveRangeAsync(productsToRemove);
        }
    }
}
