using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Models;

namespace Pricat.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración de relaciones
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade); // Eliminación en cascada opcional

        // Configuración de índices únicos
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.EanCode)
            .IsUnique();
    }
}
