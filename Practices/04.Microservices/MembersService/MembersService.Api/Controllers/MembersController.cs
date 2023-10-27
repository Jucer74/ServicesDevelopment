using AutoMapper;
using MembersService.Api.Dtos;
using MembersService.Application.Interfaces;
using MembersService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MembersService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;

        public MembersController(IMemberService teamMemberService, IMapper mapper)
        {
            _memberService = teamMemberService;
            _mapper = mapper;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var teamMembers = await _memberService.GetAllMembers() as List<Member>;
            return Ok(_mapper.Map<List<Member>, List<MemberDto>>(teamMembers!));
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            var teamMember = await _memberService.GetMemberById(id);
            return Ok(_mapper.Map<Member, MemberDto>(teamMember));
        }

        // POST api/<MembersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MemberDto teamMemberDto)
        {
            var teamMember = await _memberService.CreateMember(_mapper.Map<MemberDto, Member>(teamMemberDto));
            return Ok(_mapper.Map<Member, MemberDto>(teamMember));
        }

        // PUT api/<MembersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MemberDto teamMemberDto)
        {
            var teamMember = await _memberService.UpdateMember(id, _mapper.Map<MemberDto, Member>(teamMemberDto));
            return Ok(_mapper.Map<Member, MemberDto>(teamMember));
        }

        // DELETE api/<MembersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _memberService.DeleteMember(id);
            return Ok();
        }

        // GET api/<MembersController>/Team/5
        [HttpGet("Team/{id}")]
        public async Task<IActionResult> GetMembersByTeamId(int id)
        {
            var teamMembers = await _memberService.GetMembersByTeamId(id) as List<Member>;
            return Ok(_mapper.Map<List<Member>, List<MemberDto>>(teamMembers!));
        }
    }
}