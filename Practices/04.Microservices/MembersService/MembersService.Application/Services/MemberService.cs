using MembersService.Application.Interfaces;
using MembersService.Domain.Entities;
using MembersService.Domain.Exceptions;
using MembersService.Domain.Interfaces;
using System.Linq.Expressions;

namespace MembersService.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly ITeamRepository _teamRepository;

    public MemberService(IMemberRepository memberRepository, ITeamRepository teamRepository)
    {
        _memberRepository = memberRepository;
        _teamRepository = teamRepository;
    }

    public async Task<Member> AddAsync(Member entity)
    {
        var team = await _teamRepository.GetByIdAsync(entity.TeamId);

        if (team is null)
        {
            throw new NotFoundException($"Team with id {entity.TeamId} not found");
        }

        return await _memberRepository.AddAsync(entity);
    }

    public async Task<IEnumerable<Member>> FindAsync(Expression<Func<Member, bool>> predicate)
    {
        return await _memberRepository.FindAsync(predicate);
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        return await _memberRepository.GetAllAsync();
    }

    public async Task<Member> GetByIdAsync(int id)
    {
        Member member = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Member with Id={id} Not Found");
        return member;
    }

    public async Task RemoveAsync(int id)
    {
        Member member = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Member with Id={id} Not Found");
        await _memberRepository.RemoveAsync(member);
    }

    public async Task<Member> UpdateAsync(int id, Member entity)
    {
        if (id != entity.Id)
        {
            throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
        }

        _ = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Member with Id={id} Not Found");

        return (await _memberRepository.UpdateAsync(entity));
    }
}