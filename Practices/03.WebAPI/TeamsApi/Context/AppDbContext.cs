using Microsoft.EntityFrameworkCore;
using StudentsApp.Domain.Entities;

namespace StudentsApp.Infrastructure.Context;

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