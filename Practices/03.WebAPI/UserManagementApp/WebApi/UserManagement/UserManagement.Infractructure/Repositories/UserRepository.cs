using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;
using UserManagement.Infractructure.Persistence;
using UserManagement.Infractructure.Persistence.Context;


namespace UserManagement.Infractructure.Repositories
{
    public class UserRepository : Respository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
