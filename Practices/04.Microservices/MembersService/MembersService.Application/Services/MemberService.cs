using MembersService.Application.Interfaces;
using MembersService.Domain.Entities;
using MembersService.Domain.Exceptions;
using MembersService.Domain.Interfaces.Repositories;

namespace MembersService.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> CreateMember(Member member)
        {
            return await _memberRepository.AddAsync(member);
        }

        public async Task DeleteMember(int id)
        {
            var original = await _memberRepository.GetByIdAsync(id);

            if (original is not null)
            {
                await _memberRepository.RemoveAsync(original);
                return;
            }

            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        public async Task<IEnumerable<Member>> GetAllMembers()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member> GetMemberById(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);

            if (member is not null)
            {
                return member;
            }

            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        public async Task<Member> UpdateMember(int id, Member member)
        {
            if (id != member.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to TeamMember.Id [{member.Id}]");
            }

            var original = await _memberRepository.GetByIdAsync(id);

            if (original is not null)
            {
                return await _memberRepository.UpdateAsync(member);
            }

            throw new NotFoundException($"Team Member with Id={id} Not Found");
        }

        public async Task<IEnumerable<Member>> GetMembersByTeamId(int id)
        {
            var members = await _memberRepository.FindAsync(m => m.TeamId == id);
            return members;
        }
    }
}