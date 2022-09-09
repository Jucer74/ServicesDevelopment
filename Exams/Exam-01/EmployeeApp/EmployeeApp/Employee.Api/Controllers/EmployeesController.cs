using Employee.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Employee.BL.BusinessLogic;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeBL employeeBL;

        public EmployeesController(AppDbContext dbContext)
        {
            employeeBL = new EmployeeBL(dbContext);
        }

        // GET: api/<PersonsController>
        [HttpGet]
        public IEnumerable<Entities.Models.Employee> Get()
        {
            return employeeBL.Get();
        }

        // GET api/<PersonsController>/5
        [HttpGet("{id}")]
        public Entities.Models.Employee Get(int id)
        {
            return employeeBL.GetById(id);
        }

        // POST api/<PersonsController>
        [HttpPost]
        public void Post([FromBody] Entities.Models.Employee entity)
        {
            employeeBL.Add(entity);
        }

        // PUT api/<PersonsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Entities.Models.Employee entity)
        {
            employeeBL.Update(id, entity);
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            employeeBL.Delete(id);
        }
    }
}