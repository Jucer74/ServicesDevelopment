﻿using StudentsApp.Application.Interfaces;
using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Exceptions;
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
       
      private readonly Domain.Interfaces.Repositories.IStudentRepository _studentRepository;

        public StudentService(Domain.Interfaces.Repositories.IStudentRepository studentRepository)
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
            var student = await _studentRepository.GetByIdAsync(id);

            if (student is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            return student;
        }

        public async Task RemoveAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            await _studentRepository.RemoveAsync(student);
        }

        public async Task<Student> UpdateAsync(int id, Student entity)
        {
            if (id != entity.Id)
            {
                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }

            var student = await _studentRepository.GetByIdAsync(id);

            if (student is null)
            {
                throw new NotFoundException($"Student with Id={id} Not Found");
            }

            return (await _studentRepository.UpdateAsync(entity));
        }
    }
}