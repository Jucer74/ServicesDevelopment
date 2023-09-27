using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MembersService.Domain.Dtos;
using MembersService.Domain.Entities;
using MembersService.Application.Interfaces;

namespace MembersService.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class MembersController: ControllerBase
{
    private readonly IMemberService _MemberService;
    private readonly IMapper _mapper;

    public MembersController(IMemberService MemberService, IMapper mapper)
    {
        _MemberService = MemberService;
        _mapper = mapper;
    }

    // GET: api/<MembersController>
    [HttpGet]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _MemberService.GetAllAsync();
        return Ok(_mapper.Map<List<Member>, List<MemberDto>>((List<Member>)members));
    }

    // GET api/<MembersController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var Member = await _MemberService.GetByIdAsync(id);
        return Ok(_mapper.Map<Member, MemberDto>(Member));
    }

    // POST api/<MembersController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MemberDto MemberDto)
    {
        Member Member = await _MemberService.AddAsync(_mapper.Map<MemberDto, Member>(MemberDto));

        return Ok(_mapper.Map<Member, MemberDto>(Member));
    }

    // PUT api/<MembersController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] MemberDto MemberDto)
    {
        Member Member = await _MemberService.UpdateAsync(id, _mapper.Map<MemberDto, Member>(MemberDto));

        return Ok(_mapper.Map<Member, MemberDto>(Member));
    }

    // DELETE api/<MembersController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _MemberService.RemoveAsync(id);
        return Ok();
    }
}
