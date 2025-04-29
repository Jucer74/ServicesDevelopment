using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Models;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Pricat.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
    {
        return await _appDbContext.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    // Ejemplo de método adicional para búsqueda por EAN
    public async Task<Product?> GetByEanCodeAsync(string eanCode)
    {
        return await _appDbContext.Products
            .FirstOrDefaultAsync(p => p.EanCode == eanCode);
    }
}
