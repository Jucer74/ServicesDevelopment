using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Employee.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee.Entities.Models.Employee> Employees { get; set; }
    }
}
