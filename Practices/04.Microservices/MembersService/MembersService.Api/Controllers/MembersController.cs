using AutoMapper;
using MembersService.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using MembersService.Application.Interfaces;
using MembersService.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MembersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _MemberService;
        private readonly IMapper _mapper;

        public MembersController(IMemberService memberService, IMapper mapper)
        {
            _memberService = memberService;
            _mapper = mapper;
        }

        // GET: api/<MemberController>
        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var members = await _memberService.GetAllMembers() as List<Member>;
            return Ok(_mapper.Map<List<Member>, List<Member>>(members!));
        }

        // GET api/<MemberController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var member = await _memberService.GetMemberById(id);
            return Ok(_mapper.Map<member, Member>(member));
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Member Member)
        {
            var team = await _memberService.CreateTeam(_mapper.Map<Member, member>(member));
            return Ok(_mapper.Map<Member, Member>(Member));
        }

        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Member member)
        {
            var member = await _memberService.UpdateMember(id, _mapper.Map<Member, Member>(member));
            return Ok(_mapper.Map<Member, Member>(member));
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _memberService.DeleteMember(id);
            return Ok();
        }

    }
}