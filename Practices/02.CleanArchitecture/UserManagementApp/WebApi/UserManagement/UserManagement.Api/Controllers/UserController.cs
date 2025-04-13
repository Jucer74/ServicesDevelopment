    using System;
    using Microsoft.AspNetCore.Mvc;
    using UserManagement.Domain.Entities;
    using UserManagement.Domain.Exceptions;
    using UserManagement.Domain.Interfaces.Repositories;

namespace UserManagement.Api.Controllers;
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userService)
        {
            _userRepository = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _userRepository.GetByIdAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            return Ok(await _userRepository.AddAsync(user));
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            try
            {
                return Ok(await _userRepository.UpdateAsync(user));
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

        // DELETE api/<UserController>/5
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
