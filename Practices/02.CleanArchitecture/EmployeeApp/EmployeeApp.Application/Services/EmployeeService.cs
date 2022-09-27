using EmployeeApp.Application.Interfaces;
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

        public async Task<BadRequestException> AddAsync(BadRequestException entity)
        {
            return await _employeeRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<BadRequestException>> FindAsync(Expression<Func<BadRequestException, bool>> predicate)
        {
            return await _employeeRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<BadRequestException>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<BadRequestException> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            // Validte If Exist
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

        public async Task<BadRequestException> UpdateAsync(int id, BadRequestException entity)
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