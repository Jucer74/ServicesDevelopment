using Microsoft.AspNetCore.Mvc;
using Person.BL.BusinessLogic;
using Person.DAL.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Person.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class PersonsController : ControllerBase
   {
      private readonly PersonBL personBL;
      
      public PersonsController(AppDbContext dbContext)
      {
         personBL = new PersonBL(dbContext);
      }

      // GET: api/<PersonsController>
      [HttpGet]
      public IEnumerable<Entities.Models.Person> Get()
      {
         return personBL.Get();
      }

      // GET api/<PersonsController>/5
      [HttpGet("{id}")]
      public Entities.Models.Person Get(int id)
      {
         return personBL.GetById(id);
      }

      // POST api/<PersonsController>
      [HttpPost]
      public void Post([FromBody] Entities.Models.Person entity)
      {
         personBL.Add(entity);
      }

      // PUT api/<PersonsController>/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody] Entities.Models.Person entity)
      {
         personBL.Update(id, entity);
      }

      // DELETE api/<PersonsController>/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
         personBL.Delete(id);  
      }
   }
}
