using Arepas.Api.Dtos;
using Arepas.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Arepas.Api.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        // GET: api/<OrderDetailsController>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParams queryParams)
        {
            throw new NotImplementedException();
        }

        // GET api/<OrderDetailsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        // POST api/<OrderDetailsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDetailDto orderDetailDto)
        {
            throw new NotImplementedException();
        }

        // PUT api/<OrderDetailsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDetailDto orderDetailDto)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<OrderDetailsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}