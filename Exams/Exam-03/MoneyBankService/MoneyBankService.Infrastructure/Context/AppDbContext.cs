using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
