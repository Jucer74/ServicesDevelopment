using Microsoft.EntityFrameworkCore;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Infrastructure.Context
{
   public class AppDbContext : DbContext
   {
      public AppDbContext()
      {
      }

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
      {
      }

      public DbSet<Product> Products { get; set; }
   }
}