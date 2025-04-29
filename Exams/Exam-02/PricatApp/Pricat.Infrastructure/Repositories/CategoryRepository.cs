using Microsoft.EntityFrameworkCore;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Models;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;

namespace Pricat.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Category?> GetCategoryByIdIncludeProduct(int id)
        {
            return await _appDbContext.Categories.Include(m => m.Products).Where(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}