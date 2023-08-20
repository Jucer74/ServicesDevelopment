using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;

namespace Students.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
