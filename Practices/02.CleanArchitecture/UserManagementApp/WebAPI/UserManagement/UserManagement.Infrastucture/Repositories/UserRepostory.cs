using UserManagement.Dom.Entities;
using UserManagement.Dom.Interfaces.Repositories;
using UserManagement.Infrastucture.Common;
using UserManagement.Infrastucture.Context;

namespace UserManagement.Infrastucture.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}