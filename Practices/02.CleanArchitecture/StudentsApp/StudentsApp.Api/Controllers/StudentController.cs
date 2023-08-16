using Microsoft.AspNetCore.Mvc;
using StudentsApp.Application.Interfaces;
using StudentsApp.Domain.Entities;
using StudentsApp.Domain.Exceptions;

namespace StudentsApp.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/<PeopleController>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _studentService.GetAllAsync());
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

    // DELETE api/<StudentController>/5
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