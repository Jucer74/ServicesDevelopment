using People.Application.Interfaces;
using People.Domain.Entities;
using People.Domain.Exceptions;
using People.Domain.Interfaces.Repositories;
using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace People.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<Person> AddAsync(Person entity)
        {
            return await _personRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Person>> FindAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _personRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return person;
        }

        public async Task RemoveAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            await _personRepository.RemoveAsync(person);
        }

        public async Task<Student> UpdateAsync(int id, Student entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var person = await _personRepository.GetByIdAsync(id);

            if (person is null)
            {
                throw new NotFoundException($"Person with Id={id} Not Found");
            }

            return (await _personRepository.UpdateAsync(entity));
        }
    }
}