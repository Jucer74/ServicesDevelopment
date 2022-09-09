using Microsoft.EntityFrameworkCore;
using Employee.DAL.Context;

namespace Employee.DAL.DataAccess
{
    public class EmployeeDal
    {
        private readonly AppDbContext _dbContext;

        public EmployeeDal(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Entities.Models.Employee> Get()
        {
            return _dbContext.Employees.ToList();
        }

        public Entities.Models.Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public Entities.Models.Employee Add(Entities.Models.Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Update(Entities.Models.Employee entity)
        {
            var original = _dbContext.Employees.Find(entity.Id);
            if (original is not null)
            {
                _dbContext.Entry(original).State = EntityState.Modified;
                _dbContext.Entry(original).CurrentValues.SetValues(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(Entities.Models.Employee entity)
        {

            _dbContext.Employees.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}