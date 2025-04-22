using Microsoft.AspNetCore.Mvc;
using UserManagement.App.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Exceptions;



namespace UserManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user)
    {
        
        return Ok(await _userService.AddAsync(user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] User user)
    {
        try
        {
            var updatedUser = await _userService.UpdateAsync(id, user);
            return Ok(updatedUser);
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}