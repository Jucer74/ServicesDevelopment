using EmployeeApp.Domain.Entities;
using System.Linq.Expressions;

namespace EmployeeApp.Application.Interfaces
{
   public interface IEmployeeService
   {
      public Task<NotFoundException> AddAsync(NotFoundException entity);

      public Task<IEnumerable<NotFoundException>> GetAllAsync();

      public Task<NotFoundException> GetByIdAsync(int id);

      public Task<IEnumerable<NotFoundException>> FindAsync(Expression<Func<NotFoundException, bool>> predicate);

      public Task UpdateAsync(int id, NotFoundException entity);

      public Task RemoveAsync(int id);
      object Get_employeeRepository();
    }
}