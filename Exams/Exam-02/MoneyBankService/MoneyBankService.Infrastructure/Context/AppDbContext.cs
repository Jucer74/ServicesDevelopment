using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

<<<<<<< HEAD
    public DbSet<Account> Accounts { get; set; }
=======
    public DbSet<Account> Accounts{ get; set; }
>>>>>>> main
}