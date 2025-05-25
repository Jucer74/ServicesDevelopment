using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar índice único para AccountNumber
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();

        // Configurar precisión decimal para montos
        modelBuilder.Entity<Account>()
            .Property(a => a.BalanceAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Account>()
            .Property(a => a.OverdraftAmount)
            .HasPrecision(18, 2);
    }
}