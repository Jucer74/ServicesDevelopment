using Person.DAL.Context;
using Person.DAL.DataAccess;

namespace Person.BL.BusinessLogic
{
   public class PersonBL
   {
      private readonly PersonDal personDal;

      public PersonBL(AppDbContext dbContext)
      {
         personDal = new PersonDal(dbContext);
      }

      public IList<Entities.Models.Person> Get()
      {
         return personDal.Get();
      }

      public Entities.Models.Person GetById(int id)
      {
         return personDal.GetById(id);
      }

      public Entities.Models.Person Add(Entities.Models.Person entity)
      {
         return personDal.Add(entity);
      }


      public void Update(int id, Entities.Models.Person entity)
      {
         Entities.Models.Person entityFind = personDal.GetById(id);
         if(entityFind is not null)
         {
            personDal.Update(entity);
         }
      }

      public void Delete(int id)
      {
         Entities.Models.Person entity = personDal.GetById(id);
         personDal.Delete(entity);
      }
   }
}