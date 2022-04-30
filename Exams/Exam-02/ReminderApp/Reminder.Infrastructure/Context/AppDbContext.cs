using Microsoft.EntityFrameworkCore;
using ReminderAPP.Domain.Entities;

namespace ReminderAPP.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Reminder> Reminders { get; set; }
    }
}
