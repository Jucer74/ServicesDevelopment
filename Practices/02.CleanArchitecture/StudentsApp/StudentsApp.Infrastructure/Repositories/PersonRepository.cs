using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Interfaces.Repositories;
using StudentsApp.Infrastructure.Common;
using StudentsApp.Infrastructure.Context;

namespace StudentsApp.Infrastructure.Repositories;

public class PersonRepository : Repository<Student>, IStudentRepository
{
    public PersonRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}