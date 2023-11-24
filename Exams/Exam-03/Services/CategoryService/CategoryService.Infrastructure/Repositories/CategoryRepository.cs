using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Common;
using CategoryService.Infrastructure.Context;
using CategoryService.Domain.Entities;

namespace CategoryService.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}