using Microsoft.EntityFrameworkCore;
using ReminderApp.Domain.Entities;

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

      public DbSet<Reminder> Concepts { get; set; }
   }
}