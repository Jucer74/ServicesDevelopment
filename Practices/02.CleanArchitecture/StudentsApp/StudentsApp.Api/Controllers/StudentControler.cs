using Microsoft.AspNetCore.Mvc;
using StudentsApp.Application.Interfaces;
using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Exceptions;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentsApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentControler : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentControler(IStudentService studentService)
        {
            _studentService = studentService;
        }
        // GET: api/<StudentsControler>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PeopleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _studentService.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<PeopleController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            return Ok(await _studentService.AddAsync(student));
        }

        // PUT api/<PeopleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            try
            {
                return Ok(await _studentService.UpdateAsync(id, student));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _studentService.RemoveAsync(id);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
