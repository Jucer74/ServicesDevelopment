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

            // Configuración de Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id); // Define Id como clave primaria

                entity.Property(p => p.EanCode)
                    .IsRequired() // EanCode no puede ser nulo
                    .HasMaxLength(13); // EanCode tiene una longitud máxima de 13 caracteres

                entity.HasOne(p => p.Category) // Product tiene una relación con Category (uno a muchos)
                    .WithMany(c => c.Products) // Category puede tener muchos Products (propiedad de navegación en Category)
                    .HasForeignKey(p => p.CategoryId) // La clave foránea en Product es CategoryId
                    .OnDelete(DeleteBehavior.Cascade); // Si se borra una Category, también se borran sus Products relacionados
            });

            // Configuración de Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id); // Define Id como clave primaria

                entity.HasIndex(c => c.Description)
                    .IsUnique(); // Asegura que la Description de la Category sea única
            });
        }
    }
}