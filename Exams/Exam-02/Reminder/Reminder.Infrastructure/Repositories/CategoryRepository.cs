using ReminderAPP.Domain.Entities;
using ReminderAPP.Domain.Interface.Repositories;
using ReminderAPP.Infrastructure.Common;
using ReminderAPP.Infrastructure.Context;

namespace ReminderAPP.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
