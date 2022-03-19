namespace NetBank.DataAccess.Context
{
    using Microsoft.EntityFrameworkCore;
    using NetBank.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ReportedCard> ReportedCards { get; set; }
    }
}
