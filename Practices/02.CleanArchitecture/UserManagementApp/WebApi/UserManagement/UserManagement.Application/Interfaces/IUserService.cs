using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagement.Domain.Entities; // Asegúrate de que este espacio de nombres sea correcto y contiene la clase User  


namespace UserManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(User entity);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);
        Task<User> UpdateAsync(int id, User entity);
        Task RemoveAsync(int id);
    }
}
