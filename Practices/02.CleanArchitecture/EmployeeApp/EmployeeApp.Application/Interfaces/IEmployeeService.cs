using EmployeeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmployeeApp.Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<Employee> AddAsync(Employee entity);

        public Task<IEnumerable<Employee>> GetAllAsync();

        public Task<Employee> GetByIdAsync(int id);

        public Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate);

        public Task<Employee> UpdateAsync(int id, Employee entity);

        public Task RemoveAsync(int id);
    }
}