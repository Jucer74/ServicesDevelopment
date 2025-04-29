using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.App.Dtos;
using UserManagement.App.Interfaces;
using UserManagement.Dom.Entities;

namespace UserManagement.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        // GET: api/v1/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllAsync(); // Devuelve IEnumerable<User>
            return Ok(_mapper.Map<List<UserDto>>(users.ToList())); 
        }

        // GET: api/v1/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userServices.GetByIdAsync(id);
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        // POST: api/v1/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
        {
            var user = await _userServices.AddAsync(_mapper.Map<UserDto, User>(dto));
            return Ok(_mapper.Map<User, UserDto>(user));
        }

        // PUT: api/v1/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto dto)
        {
            var updatedUser = await _userServices.UpdateAsync(id, _mapper.Map<UserDto, User>(dto));
            return Ok(_mapper.Map<User, UserDto>(updatedUser));
        }

        // DELETE: api/v1/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userServices.RemoveAsync(id);
            return Ok();
        }
    }
}