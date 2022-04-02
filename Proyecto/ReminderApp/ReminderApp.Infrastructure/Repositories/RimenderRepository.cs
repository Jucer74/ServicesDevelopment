using ReminderApp.Domain.Entities;
using ReminderApp.Infraestucture.Context;
using ReminderApp.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp.Infrastructure.Repositories
{
    public class RimenderRepository : Repository<Reminder>
    {
        public RimenderRepository(AppDBContext appDbContext) : base(appDbContext)
        {
        }
        public IEnumerable<Reminder>FindReminderByCategory(Category category)
        {
            return base.Find(c => c.Category.Equals(category));
        }
    }
}
