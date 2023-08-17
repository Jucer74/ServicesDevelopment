using StudentsApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _StudentRepository;

        public StudentService(IStudentRepository StudentRepository)
        {
            _StudentRepository = StudentRepository;
        }

        public async Task<Student> AddAsync(Student entity)
        {
            return await _StudentRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _StudentRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _StudentRepository.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var Student = await _StudentRepository.GetByIdAsync(id);

            if (Student is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            return Student;
        }

        public async Task RemoveAsync(int id)
        {
            var Student = await _StudentRepository.GetByIdAsync(id);

            if (Student is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            await _StudentRepository.RemoveAsync(Student);
        }

        public async Task<Student> UpdateAsync(int id, Student entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var person = await _StudentRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            return (await _StudentRepository.UpdateAsync(entity));
        }
    }
}
