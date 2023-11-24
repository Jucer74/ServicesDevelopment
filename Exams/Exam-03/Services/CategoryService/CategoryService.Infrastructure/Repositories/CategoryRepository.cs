using CategoryService.Domain.Entities;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Infrastructure.Common;
using CategoryService.Infrastructure.Context;

namespace CategoryService.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}