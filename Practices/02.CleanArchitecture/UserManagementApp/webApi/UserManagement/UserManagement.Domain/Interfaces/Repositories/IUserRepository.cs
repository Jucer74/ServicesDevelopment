using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Interfaces.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
       
    }
}
