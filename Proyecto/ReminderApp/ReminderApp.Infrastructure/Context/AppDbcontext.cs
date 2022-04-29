using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Entities;
using System;

namespace ReminderApp.Infrastructure.Context
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

        internal object Where(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}