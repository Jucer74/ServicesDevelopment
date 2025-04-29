using Pricat.Domain.Models;
using Pricat.Infrastructure.Common;
using Pricat.Infrastructure.Context;
using Pricat.Application.Interfaces.Repositories;
namespace Pricat.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
        {
            public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
            {

            }
        }
    
    
}