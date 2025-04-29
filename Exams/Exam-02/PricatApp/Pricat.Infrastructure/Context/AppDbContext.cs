using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relación uno a muchos: Una categoría tiene muchos productos
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category) // Un producto tiene una categoría
            .WithMany(c => c.Products) // Una categoría tiene muchos productos
            .HasForeignKey(p => p.CategoryId) // Clave foránea en Product
            .OnDelete(DeleteBehavior.Cascade); // Elimina productos al eliminar una categoría
    }
}