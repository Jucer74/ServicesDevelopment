using EmployeeApp.Application.Interfaces;
using EmployeeApp.Domain.Entities;
using EmployeeApp.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace EmployeeApp.Application.Services
{
   public class EmployeeService : IEmployeeService
   {
      private readonly IEmployeeRepository _EmployeeRepository;

      public EmployeeService(IEmployeeRepository EmployeeRepository)
      {
         _EmployeeRepository = EmployeeRepository;
      }

      public async Task<Employee> AddAsync(Employee entity)
      {
         return await _EmployeeRepository.AddAsync(entity);
      }

      public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
      {
         return await _EmployeeRepository.FindAsync(predicate);
      }

      public async Task<IEnumerable<Employee>> GetAllAsync()
      {
         return await _EmployeeRepository.GetAllAsync();
      }

      public async Task<Employee> GetByIdAsync(int id)
      {
         var Employee = await _EmployeeRepository.GetByIdAsync(id);

         // Validte If Exist
         return Employee;
      }

      public async Task RemoveAsync(int id)
      {
         var Employee = await _EmployeeRepository.GetByIdAsync(id);
         await _EmployeeRepository.RemoveAsync(Employee);
      }

      public async Task UpdateAsync(int id, Employee entity)
      {
         // Validate if Exist
         await _EmployeeRepository.UpdateAsync(entity);
      }
   }
}