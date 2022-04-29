using ReminderApp.Domain.Entities;
using ReminderApp.Domain.Interfaces.Repositories;
using ReminderApp.Infrastructure.Common;
using ReminderApp.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReminderApp.Infrastructure.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReminderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task  DeleteAllByCategoryId(int id)
        {
            var categoria = _appDbContext.Reminders.Where(x => x.CategoryId == id).ToList();
            _appDbContext.RemoveRange(categoria);
            await _appDbContext.SaveChangesAsync();
        }
        public Task<List<Reminder>> GetAllBycategoryId(int id)
        {
            return Task.FromResult(_appDbContext.Reminders.Where(x => x.CategoryId == id).ToList());
        }
        public IEnumerable<Reminder> FindRemindersByCategory(Category category)
        {
            return (IEnumerable<Reminder>)base.Find(c => c.Category.Equals(category));
        }
    }
}