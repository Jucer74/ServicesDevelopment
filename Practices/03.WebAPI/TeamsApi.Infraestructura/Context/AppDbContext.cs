using Microsoft.EntityFrameworkCore;
using TeamsApi.Dominio.Models;

namespace TeamsApi.Infraestructura.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
}