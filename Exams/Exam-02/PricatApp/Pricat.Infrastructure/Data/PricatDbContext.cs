using Microsoft.EntityFrameworkCore;

using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Data
{
    public class PricatDbContext : DbContext
    {
        public PricatDbContext(DbContextOptions<PricatDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.Property(e => e.Description)
                      .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.Property(e => e.EanCode)
                      .HasColumnType("varchar(13)");
            });
        }
    }
}