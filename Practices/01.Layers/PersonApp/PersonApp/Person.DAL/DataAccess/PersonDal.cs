using Microsoft.EntityFrameworkCore;
using Person.DAL.Context;

namespace Person.DAL.DataAccess
{
   public class PersonDal
   {
      private readonly AppDbContext _dbContext;

      public PersonDal(AppDbContext dbContext)
      {
         _dbContext = dbContext;
      }

      public IList<Entities.Models.Person> Get()
      {
         return _dbContext.Persons.ToList();
      }

      public Entities.Models.Person GetById(int id)
      {
         return _dbContext.Persons.Find(id);
      }

      public Entities.Models.Person Add(Entities.Models.Person entity)
      {
         _dbContext.Persons.Add(entity);
         _dbContext.SaveChanges();
         return entity;
      }

      public void Update(Entities.Models.Person entity)
      {
         _dbContext.Entry(entity).State = EntityState.Modified;
         _dbContext.SaveChanges();
      }

      public void Delete(Entities.Models.Person entity)
      {

         _dbContext.Persons.Remove(entity);
         _dbContext.SaveChanges();
      }
   }
}