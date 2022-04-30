using Microsoft.AspNetCore.Mvc;
using ReminderApp.Application.Interfaces;
using ReminderApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReminderApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reminderService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _reminderService.GetById(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post( Reminder _reminder)
        {
            await _reminderService.Add(_reminder);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,  Reminder _reminder)
        {
            await _reminderService.Update(id, _reminder);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _reminderService.Remove(id);
            return Ok();
        }

        [HttpDelete("Category/{id}")]
        public async Task DeleteAllByCategoryId(int id)
        {
            await _reminderService.DeleteAllByCategoryId(id);
        }
        [HttpGet("Category/{id}")]
        public async Task<IEnumerable<Reminder>> GetAllBycategoryId(int id)
        {
            return await _reminderService.GetAllBycategoryId(id);

        }

    }
}
