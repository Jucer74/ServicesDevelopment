using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Domain.Entities;
using CategoryService.Infrastructure.Common;
using CategoryService.Infrastructure.Context;
using CategoryService.Domain.Dtos;

namespace CategoryService.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}