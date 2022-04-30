using ReminderApp.Domain.Common;
using ReminderAPP.Domain.Entities;
using ReminderAPP.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReminderAPP.Domain.Interface.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
