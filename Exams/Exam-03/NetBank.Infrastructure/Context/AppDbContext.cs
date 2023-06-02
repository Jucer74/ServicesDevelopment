using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Models;

namespace NetBank.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<IssuingNetwork> IssuingNetworks { get; set; }
}