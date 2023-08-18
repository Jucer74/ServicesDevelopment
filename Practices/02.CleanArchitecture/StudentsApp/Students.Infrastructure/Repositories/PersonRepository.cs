using Students.Domain.Entities;
using Students.Domain.Interfaces.Repositories;
using Students.Infrastructure.Common;
using Students.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Infrastructure.Repositories
{
    public class PersonRepository : Repository<Student>, IStudentRepository
    {
        public PersonRepository(AppDbContext appDbContext):base(appDbContext) { }
    }
}
