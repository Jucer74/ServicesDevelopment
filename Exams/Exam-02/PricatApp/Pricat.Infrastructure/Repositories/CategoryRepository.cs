using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using Pricat.Infrastructure.Data;
using Pricat.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Pricat.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private new readonly PricatDbContext _context;

        public CategoryRepository(PricatDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products)  // Carga relacionada
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task DeleteAsync(Category entity)
        {
            // Eliminación en cascada manual (opcional)
            var products = await _context.Products
                .Where(p => p.CategoryId == entity.Id)
                .ToListAsync();

            _context.Products.RemoveRange(products);
            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();
        }

        Task<bool> ICategoryRepository.DeleteAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
