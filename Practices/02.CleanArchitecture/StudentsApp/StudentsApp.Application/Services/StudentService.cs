using StudentsApp.Application.Interfaces;
using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Exceptions;
using StudentsApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Application.Services
{
    internal class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> AddAsync(Student entity)
        {
            return await _studentRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _studentRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var person = await _studentRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return person;
        }

        public async Task RemoveAsync(int id)
        {
            var person = await _studentRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            await _studentRepository.RemoveAsync(person);
        }

        public async Task<Student> UpdateAsync(int id, Student entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var person = await _studentRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return (await _studentRepository.UpdateAsync(entity));
        }
    }
}
