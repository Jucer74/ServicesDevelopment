using UserManagement.Domain.Entities;
using UserManagement.Infrastr.Common;
using UserManagement.Infrastr.Context;
using UserManagement.Domain.Interfaces.Repositories;

namespace UserManagement.Infrastr.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
