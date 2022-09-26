using EmployeeApp.Domain.Entities;
using EmployeeApp.Domain.Interfaces.Repositories;
using EmployeeApp.Infrastructure.Common;
using EmployeeApp.Infrastructure.Context;

namespace EmployeeApp.Infrastructure.Repositories
{
   public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
   {
      public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
      {
      }
   }
}