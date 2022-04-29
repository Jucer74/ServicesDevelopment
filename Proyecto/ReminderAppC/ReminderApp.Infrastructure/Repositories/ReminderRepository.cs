using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Context;
using System.Collections.Generic;

namespace ReminderApp.Infrastructure.Repositories
{
   public class ReminderRepository : Repository<Reminder>, IReminderRepository
   {
      public ReminderRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }

      public IEnumerable<Reminder> FindRemindersByCategory(Category category)
      {
         return (IEnumerable<Reminder>)base.FindAsync(c => c.Category.Equals(category));
      }
   }
}