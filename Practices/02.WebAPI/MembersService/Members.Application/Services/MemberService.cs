using Members.Application.Interfaces;
using Members.Domain.Entities;
using Members.Domain.Exceptions;
using Members.Domain.Interfaces;
using System.Linq.Expressions;

namespace Members.Application.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;

    public MemberService(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Member> AddAsync(Member entity)
    {
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

        Member member = await _memberRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Member with Id={id} Not Found");

        return (await _memberRepository.UpdateAsync(entity));
    }
}