using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Dtos.Users;
using Users.Application.Exceptions;
using Users.Application.Interfaces;
using Users.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Users.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(_mapper.Map<List<User>, List<UserDtoOutput>>((List<User>)users));
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(_mapper.Map<User, UserDtoOutput>(user));
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDtoInput userDto)
        {
            var user = await _userService.AddAsync(_mapper.Map<UserDtoInput, User>(userDto));
            return Ok(_mapper.Map<User, UserDtoOutput>(user));
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDtoInput userDto)
        {
            var user = await _userService.UpdateAsync(id, _mapper.Map<UserDtoInput, User>(userDto));
            return Ok(_mapper.Map<User, UserDtoOutput>(user));
        }

        // DELETE api/<PeopleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}