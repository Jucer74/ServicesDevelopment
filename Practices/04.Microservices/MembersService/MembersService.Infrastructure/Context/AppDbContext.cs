using MembersService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MembersService.Infrastructure.Context;

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