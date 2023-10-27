using MembersService.Domain.Entities;

namespace MembersService.Application.Interfaces;

public interface IMemberService
{
    Task<Member> CreateMember(Member team);

    Task DeleteMember(int id);

    Task<IEnumerable<Member>> GetAllMembers();

    Task<Member> GetMemberById(int id);

    Task<Member> UpdateMember(int id, Member team);

    Task<IEnumerable<Member>> GetMembersByTeamId(int id);
}