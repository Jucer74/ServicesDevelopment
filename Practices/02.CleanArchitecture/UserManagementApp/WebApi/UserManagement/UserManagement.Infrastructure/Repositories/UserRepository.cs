using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Persistence.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext _context) : base(_context)
        {
        }
    }
}