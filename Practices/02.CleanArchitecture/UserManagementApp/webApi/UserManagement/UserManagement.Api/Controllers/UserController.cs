using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Exceptions;
using System.Threading.Tasks;
using System;
namespace UserManagement.Api.Controllers
{
   
        [Route("api/v1/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            // GET
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _userService.GetAllAsync());
            }

            // GET api/<PeopleController>/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                try
                {
                    return Ok(await _userService.GetByIdAsync(id));
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }

            // POST api/<PeopleController>
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] User user)
            {
                return Ok(await _userService.AddAsync(user));
            }

            // PUT api/<PeopleController>/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] User user)
            {
                try
                {
                    return Ok(await _userService.UpdateAsync(id, user));
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
                    await _userService.RemoveAsync(id);
                    return Ok();
                }
                catch (NotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }

        }
    }


