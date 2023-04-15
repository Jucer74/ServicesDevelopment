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

    public DbSet<TeamDto> Teams { get; set; }
    public DbSet<TeamMemberDto> TeamMembers { get; set; }
}