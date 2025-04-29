using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Context
{
    public class PricatAppDbContext : DbContext
    {
        public PricatAppDbContext(DbContextOptions<PricatAppDbContext> options) : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
