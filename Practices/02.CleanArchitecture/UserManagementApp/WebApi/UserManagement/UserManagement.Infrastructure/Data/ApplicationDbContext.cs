using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}