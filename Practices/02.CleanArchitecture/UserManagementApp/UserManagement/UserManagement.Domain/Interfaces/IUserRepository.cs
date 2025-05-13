using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> EmailExistsAsync(string email);
    }
}