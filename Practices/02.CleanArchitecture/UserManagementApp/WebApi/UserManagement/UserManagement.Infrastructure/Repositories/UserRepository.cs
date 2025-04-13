﻿using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;
using UserManagement.Infrastructure.Common;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext _context) : base(_context)
        {
        }
    }
}