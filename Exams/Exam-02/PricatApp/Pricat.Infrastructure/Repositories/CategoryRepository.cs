using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Common;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Persistence.Context;

namespace Pricat.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PricatDbContext _context;

        public CategoryRepository(PricatDbContext context)
        {
            _context = context;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category; // Correcto: Retorna el objeto categoria agregado
        }

        public async Task UpdateAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory == null)
                throw new Exception("La categoría no existe.");

            existingCategory.Description = category.Description; // Solo actualiza campos que quieras cambiar

            await _context.SaveChangesAsync(); // Listo, sin conflictos de tracking
        }


        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
