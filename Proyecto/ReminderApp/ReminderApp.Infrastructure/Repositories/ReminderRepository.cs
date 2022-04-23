using ReminderApp.Domain.Entities;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Context;
using System.Collections.Generic;

namespace ReminderApp.Infrastructure.Repositories
{
   public class ReminderRepository : Repository<Reminder>
   {
      public ReminderRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }

      public IEnumerable<Reminder> FindRemindersByCategory(Category category)
      {
         return base.Find(c => c.Category.Equals(category));
      }
   }
}