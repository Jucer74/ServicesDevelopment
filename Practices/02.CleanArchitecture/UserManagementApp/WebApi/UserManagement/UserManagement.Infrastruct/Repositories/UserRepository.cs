using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.Infrastruct.Common;
using UserManagement.Infrastruct.Context;

namespace UserManagementPeople.Infrastruct.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}