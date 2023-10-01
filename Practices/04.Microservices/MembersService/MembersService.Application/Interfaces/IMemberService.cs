using MembersService.Domain.Entities;

namespace MembersService.Application.Interfaces;
public interface IMemberService
{ 
    Task<Member> CreateMember(Member member);

    Task DeleteMember(int id);

    Task<IEnumerable<Member>> GetAllMembers();

    Task<Member> GetMemberById(int id);

    Task<Member> UpdateMember(int id, Member member);

    Task<IEnumerable<Member>> GetMembersByTeamId(int id);
}