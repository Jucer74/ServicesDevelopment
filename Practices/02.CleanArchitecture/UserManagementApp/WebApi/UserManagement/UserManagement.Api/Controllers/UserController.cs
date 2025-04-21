using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfeces.Repositories;

namespace UserManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        //GET : api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try 
            {
                return Ok(await _userRepository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            return Ok(await _userRepository.AddAsync(user));
        }

        // PUT: api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            try 
            {
                return Ok(await _userRepository.UpdateAsync(user));
            }
            catch (BadHttpRequestException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // DELETE: api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user is null)
                {
                    return NotFound("User not found");
                }

                await _userRepository.RemoveAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
