using Microsoft.AspNetCore.Mvc;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReminderApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        // GET: api/<PeopleController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reminderService.GetAllAsync());
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _reminderService.GetByIdAsync(id));
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reminder person)
        {
            await _reminderService.AddAsync(person);
            return Ok();
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Reminder person)
        {
            await _reminderService.UpdateAsync(id, person);
            return Ok();
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reminderService.RemoveAsync(id);
            return Ok();
        }
    }
}
