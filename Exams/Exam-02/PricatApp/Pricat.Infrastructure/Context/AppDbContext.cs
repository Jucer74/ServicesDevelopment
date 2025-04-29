// Importación de los namespaces necesarios
using Microsoft.EntityFrameworkCore;  // Para trabajar con Entity Framework Core
using Pricat.Domain.Models;  // Para importar los modelos de dominio (por ejemplo, Category y Product)

namespace Pricat.Infrastructure.Context
{
    // Clase que representa el contexto de la base de datos, heredando de DbContext
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración para el contexto de base de datos y las pasa al constructor base
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Propiedades DbSet que representan las tablas de la base de datos para las entidades Category y Product
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Método que se usa para configurar el modelo de la base de datos, como relaciones, restricciones, índices, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Product y Category
            // Cada producto tiene una categoría (relación uno a muchos)
            // Si se elimina una categoría, se eliminarán todos los productos relacionados con ella (borrado en cascada)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)  // Un producto tiene una categoría
                .WithMany(c => c.Products)  // Una categoría puede tener muchos productos
                .HasForeignKey(p => p.CategoryId)  // La clave foránea en la entidad Product es CategoryId
                .OnDelete(DeleteBehavior.Cascade);  // Borrado en cascada, cuando se borra una categoría se borran sus productos

            // Configuración para crear un índice único en el campo EanCode de los productos
            // Esto garantiza que no haya dos productos con el mismo código EAN en la base de datos
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.EanCode)  // Se crea un índice en la columna EanCode
                .IsUnique();  // El índice debe ser único, no se permiten duplicados
        }
    }
}
