using Microsoft.EntityFrameworkCore;
using EmployeeApp.Domain.Entities;

namespace EmployeeApp.Infrastructure.Context
{
   public class AppDbContext : DbContext
   {
      public AppDbContext()
      {
      }

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
      {
      }

      public DbSet<Employee> Employees { get; set; }
   }
}