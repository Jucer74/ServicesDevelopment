using StudentsApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Application.Interfaces
{
    public interface IStudentService
    {
        public Task<Student> AddAsync(Student entity);

        public Task<IEnumerable<Student>> GetAllAsync();

        public Task<Student> GetByIdAsync(int id);

        public Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate);

        public Task<Student> UpdateAsync(int id, Student entity);

        public Task RemoveAsync(int id);
    }
}
