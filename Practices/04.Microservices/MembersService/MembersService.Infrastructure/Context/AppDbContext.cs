using Microsoft.EntityFrameworkCore;
using MembersService.Domain.Entities;

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

        public DbSet<Member> Members { get; set; }
    }
}