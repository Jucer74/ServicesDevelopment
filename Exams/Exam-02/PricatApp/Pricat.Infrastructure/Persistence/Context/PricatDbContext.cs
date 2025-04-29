using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Common;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Persistence.Context
{
    public class PricatDbContext : DbContext
    {
        public PricatDbContext(DbContextOptions<PricatDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Puedes configurar más detalles de las entidades si lo necesitas
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}