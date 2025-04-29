using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;  // Ajusta el namespace según tu carpeta de Entities

namespace Pricat.Infrastructure.Context
{
    public class PricatDbContext : DbContext
    {
        public PricatDbContext(DbContextOptions<PricatDbContext> options)
            : base(options)
        {
        }

        // Aquí defines tus tablas a partir de las entidades del dominio
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        // (Opcional) Si quieres personalizar nombres de tablas o relaciones:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ejemplo: forzar nombre de tabla
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Product>().ToTable("Products");

            // Aquí podrías configurar índices, claves foráneas, etc.
        }
    }
}