using EmployeeApp.Application.Interfaces;
using EmployeeApp.Domain.Entities;
using EmployeeApp.Domain.Exceptions;
using EmployeeApp.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace EmployeeApp.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            return await _employeeRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            return await _employeeRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException($"Employee with Id={id} Not Found");
            }

            return employee;
        }

        public async Task RemoveAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException($"Employee with Id={id} Not Found");
            }

            await _employeeRepository.RemoveAsync(employee);
        }

        public async Task<Employee> UpdateAsync(int id, Employee entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException($"Employee with Id={id} Not Found");
            }
            return (await _employeeRepository.UpdateAsync(entity));
        }
    }
}