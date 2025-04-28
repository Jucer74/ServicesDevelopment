using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Exceptions;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Application.Mappers;
using UserManagement.Domain.Entities;

namespace UserManagement.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepository.GetAllAsync();
        var userDtos = users.Select(UserMapper.ToDto);
        return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDto = UserMapper.ToDto(user);
            return Ok(userDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto createUserDto)
    {
        var user = new User
        {
            Email = createUserDto.Email,
            FullName = createUserDto.FullName,
            UserName = createUserDto.UserName,
            Password = createUserDto.Password // cuidado: idealmente deberías hashear aquí
        };

        var createdUser = await _userRepository.AddAsync(user);
        var userDto = UserMapper.ToDto(createdUser);
        return Ok(userDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Email = updateUserDto.Email;
            user.FullName = updateUserDto.FullName;
            user.UserName = updateUserDto.UserName;

            var updatedUser = await _userRepository.UpdateAsync(user);
            var userDto = UserMapper.ToDto(updatedUser);
            return Ok(userDto);
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
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.RemoveAsync(user);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
