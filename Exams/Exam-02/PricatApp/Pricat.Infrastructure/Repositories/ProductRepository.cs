using Microsoft.EntityFrameworkCore;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Persistence;
using Pricat.Infrastructure.Persistence.Context;

namespace Pricat.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(PricatDbContext _context) : base(_context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                                 .Where(p => p.CategoryId == categoryId)
                                 .ToListAsync();
        }
    }
}