using Microsoft.AspNetCore.Mvc;
using Students.Application.Interfaces;
using Students.Domain.Entities;
using Students.Domain.Exceptions;
using System;

namespace Students.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController (IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult>GetAll()
    {
        return Ok(await _studentService.GetAllAsync());
    }

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
