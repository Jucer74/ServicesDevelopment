using Microsoft.EntityFrameworkCore;
using PricatApp.Domain.Entities;

namespace PricatApp.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AppDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Categorie> Categories { get; set; }

    }
}