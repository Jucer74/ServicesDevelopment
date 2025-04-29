using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Data;

namespace Pricat.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private new readonly PricatDbContext _context;

        public ProductRepository(PricatDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<bool> IsEanCodeUniqueAsync(string eanCode, int? excludedId = null)
        {
            return await _context.Products
                .Where(p => p.EanCode == eanCode && (excludedId == null || p.Id != excludedId))
                .CountAsync() == 0;
        }

        public override async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)  // Carga relacionada
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)  // Carga relacionada
                .ToListAsync();
        }

        Task<bool> IProductRepository.DeleteAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}