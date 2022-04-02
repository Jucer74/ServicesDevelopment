using ReminderApp.Domain.Entities;
using ReminderApp.Infraestucture.Context;
using ReminderApp.Infrastructure.Common;


namespace ReminderApp.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }
    }
}
