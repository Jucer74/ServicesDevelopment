using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace UserManagement.Infrastructure.Persistence.Context

{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();
            optionsBuilder.UseSqlite("Data Source=usermanagement.db");

            return new UserDbContext(optionsBuilder.Options);
        }
    }
}