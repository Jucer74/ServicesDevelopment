using Microsoft.EntityFrameworkCore;

namespace Person.DAL.Context
{
   public class AppDbContext : DbContext
   {
      public AppDbContext()
      {
      }

      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
      {
      }

      public DbSet<Entities.Models.Person> Persons { get; set; }
   }
}