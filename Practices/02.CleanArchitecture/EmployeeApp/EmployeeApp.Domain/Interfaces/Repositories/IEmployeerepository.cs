using EmployeeApp.Domain.Common;
using EmployeeApp.Domain.Entities;

namespace EmployeeApp.Domain.Interfaces.Repositories
{
   public interface IEmployeeRepository : IRepository<Employee>
   {
   }
}