using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Entities;

namespace Pricat.Infrastructure.Persistence.Context
{
    public class PricatDbContext : DbContext
    {
        public PricatDbContext()
        {
        }

        public PricatDbContext(DbContextOptions<PricatDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
