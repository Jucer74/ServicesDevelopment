using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities; 

namespace MoneyBankService.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts"); 
            });
        }



    }
}