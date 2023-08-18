using StudentsApp.Domain.Common;
using StudentsApp.Domain.Entities;
using System.Linq.Expressions;

namespace StudentsApp.Domain.Interfaces.Repositories;

    public interface IStudentRepository : IRepository<Student>
{
}
