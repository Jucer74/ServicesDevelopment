using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReminderApp.Infrastructure.Repositories
{
   public class ReminderRepository : Repository<Reminder>, IReminderRepository
   {
      public ReminderRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }

      public Task<IEnumerable<Reminder>> FindReminderCategory(int id)
       {
           return (Task<IEnumerable<Reminder>>)base.FindAsync(c => c.CategoryId.Equals(id));
       }
    }
}