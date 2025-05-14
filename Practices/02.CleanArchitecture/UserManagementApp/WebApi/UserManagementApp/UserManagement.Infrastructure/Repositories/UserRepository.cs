using People.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositores;
using UserManagement.Infrastructure.Common;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
