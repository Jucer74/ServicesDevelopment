using ReminderApp.Domain.Entities;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Context;

namespace ReminderApp.Infrastructure.Repositories
{
   public class CategoryRepository : Repository<Category>
   {
      public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }
   }
}