using Microsoft.EntityFrameworkCore;

using TeamsServie.Domain.Entities;

namespace TeamsService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
    }
}