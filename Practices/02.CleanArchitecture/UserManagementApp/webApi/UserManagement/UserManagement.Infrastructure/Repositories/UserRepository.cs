using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Common;
using UserManagement.Infrastructure.Context;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces.Repositories;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
    

}

