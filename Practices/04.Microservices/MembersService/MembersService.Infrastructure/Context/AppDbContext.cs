using Microsoft.EntityFrameworkCore;
using TeamsService.Domain.Entities;

namespace MembersService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
    }
}