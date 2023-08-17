using StudentsApp.Infrastructure.Common;
using StudentsApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApp.Infrastructure.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
