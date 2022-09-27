using Microsoft.EntityFrameworkCore;
using EmployeeApp.Domain.Entities;
using System.Collections.Generic;

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

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        internal object Set<T>()
        {
            throw new NotImplementedException();
        }
    }
}