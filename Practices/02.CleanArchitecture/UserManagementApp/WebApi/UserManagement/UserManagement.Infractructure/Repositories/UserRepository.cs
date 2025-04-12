using UserManagement.Domain.Entities;
using UserManagement.Infractructure.Common;
using UserManagement.Infractructure.Context;
using UserManagement.Domain.Interfeces.Repositories;

namespace UserManagement.Infractructure.Repositories
{
    public class UserRepository : Respository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
