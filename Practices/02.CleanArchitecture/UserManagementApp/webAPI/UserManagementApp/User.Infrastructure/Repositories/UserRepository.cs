using Users.Domain.Interfaces.Repositories;
using Users.Domain.Entities;
using Users.Infrastructure.Context;
using Users.Infrastructure.Common;


namespace Users.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
