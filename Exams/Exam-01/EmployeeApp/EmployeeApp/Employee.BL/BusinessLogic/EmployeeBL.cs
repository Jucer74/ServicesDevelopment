using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee.DAL.Context;
using Employee.DAL.DataAccess;

namespace Employee.BL.BusinessLogic
{
    public class EmployeeBL
    {
        private readonly EmployeeDal employeeDal;

        public EmployeeBL(AppDbContext dbContext)
        {
            employeeDal = new EmployeeDal(dbContext);
        }

        public IList<Entities.Models.Employee> Get()
        {
            return employeeDal.Get();
        }

        public Entities.Models.Employee GetById(int id)
        {
            return employeeDal.GetById(id);
        }

        public Entities.Models.Employee Add(Entities.Models.Employee entity)
        {
            return employeeDal.Add(entity);
        }


        public void Update(int id, Entities.Models.Employee entity)
        {
            Entities.Models.Employee entityFind = employeeDal.GetById(id);
            if (entityFind is not null)
            {
                employeeDal.Update(entity);
            }
        }

        public void Delete(int id)
        {
            Entities.Models.Employee entity = employeeDal.GetById(id);
            employeeDal.Delete(entity);
        }
    }
}