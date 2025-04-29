<<<<<<< HEAD
﻿using System;
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
=======
﻿using System.Linq.Expressions;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IUserService
{
    public Task AddAsync(User entity);

    public Task<IEnumerable<User>> GetAllAsync();

    public Task<User> GetByIdAsync(int id);

    public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate);

    public Task UpdateAsync(int id, User entity);

    public Task RemoveAsync(int id);
}
>>>>>>> 9237d79b97201f1bd3534a97b9be8de15fcf8759
