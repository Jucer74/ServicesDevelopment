using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;
using System;

namespace MoneyBankService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BankAccount> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Si se detecta que se ejecuta en modo diseño, se puede utilizar una cadena de conexión local:
                string connectionString = Environment.GetEnvironmentVariable("EF_DESIGN_CONNECTION")
                    ?? "Server=mysql-db;Database=moneybankdb;User=root;Password=juan234ego;";

                optionsBuilder.UseMySql(
                    connectionString,
                    new MySqlServerVersion(new Version(8, 0, 42)),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                );
            }
        }
    }
}
