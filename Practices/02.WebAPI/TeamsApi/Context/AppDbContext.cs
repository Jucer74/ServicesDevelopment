using Microsoft.EntityFrameworkCore;
using TeamsApi.Models;

namespace TeamsApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> Members { get; set; }
}