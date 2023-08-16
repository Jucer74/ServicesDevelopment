using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Interfaces.Repositories;
using StudentsApp.Infrastructure.Common;
using StudentsApp.Infrastructure.Context;

namespace StudentsApp.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}