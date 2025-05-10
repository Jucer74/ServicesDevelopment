using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _personService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var person = await _personService.GetByIdAsync(id);
        return person != null ? Ok(person) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Person person)
    {
        var createdPerson = await _personService.AddAsync(person);
        return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Person person)
    {
        if (id != person.Id) return BadRequest();
        await _personService.UpdateAsync(person);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _personService.DeleteAsync(id);
        return NoContent();
    }
}