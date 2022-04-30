using ReminderAPP.Domain.Entities;
using ReminderAPP.Domain.Interface.Repositories;
using ReminderAPP.Infrastructure.Common;
using ReminderAPP.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReminderAPP.Infrastructure.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Reminder>> getAllByCategoryId(int Id)
        {
            return await FindAsync(x => x.Id == Id);
        }

        public async Task DeleteByCategoryId(int Id)
        {
            var remindersSet = await getAllByCategoryId(Id);
            foreach (var item in remindersSet)
            {
                await RemoveAsync(item);
            }
        }
    }
}
