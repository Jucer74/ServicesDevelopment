using Student.Domain.Common;
using Student.Domain.Entities;

namespace Student.Domain.Interfaces.Repositories
{
   public interface IStudentRepository : IRepository<Entities.Student>
   {
   }
}