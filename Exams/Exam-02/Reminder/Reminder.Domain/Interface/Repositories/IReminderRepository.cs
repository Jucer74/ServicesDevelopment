using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReminderApp.Domain.Common;
using ReminderAPP.Domain.Entities;

namespace ReminderAPP.Domain.Interface.Repositories
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        public Task<IEnumerable<Reminder>> getAllByCategoryId(int Id);

        public Task DeleteByCategoryId(int Id);
    }


}
