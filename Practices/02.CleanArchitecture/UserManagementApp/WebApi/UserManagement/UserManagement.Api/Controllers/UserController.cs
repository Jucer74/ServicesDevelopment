using Application.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces.Repositories;
using UserManagement.Domain.Entities;


namespace UserManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(_mapper.Map<List<UserDTO>>(users));
        }

        //GET : api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new NotFoundException("User not found");
            }
            return Ok(user);
        }

        // POST: api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user is null)
            {
                throw new BadRequestException("User is null");
            }
            var addUser = await _userRepository.AddAsync(user);

            return Ok(addUser);

        }

        // PUT: api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user is null)
            {
                throw new BadRequestException("User is null");
            }
            user.Id = id;
            var updateUser = await _userRepository.UpdateAsync(user);
            return Ok(updateUser);

        }
        // DELETE: api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            await _userRepository.RemoveAsync(user);
            return Ok();   
        }

    }
}
