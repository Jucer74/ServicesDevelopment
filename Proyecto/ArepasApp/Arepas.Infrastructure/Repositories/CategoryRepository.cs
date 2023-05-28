using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;

namespace Arepas.Infrastructure.Repositories
{
   public class CategoryRepository : Repository<Category>, ICategoryRepository
   {
      public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }
   }
}