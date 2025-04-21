using Microsoft.EntityFrameworkCore;
using UserManagement.Dom.Entities;

namespace UserManagement.Infrastucture.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}