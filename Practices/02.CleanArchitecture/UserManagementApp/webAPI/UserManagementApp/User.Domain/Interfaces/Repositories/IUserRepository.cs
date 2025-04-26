using Users.Domain.Common;
using Users.Domain.Entities;

namespace Users.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
    }
}