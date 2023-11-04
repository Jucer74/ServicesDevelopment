using MembersService.Domain.Entities;
using MembersService.Domain.Interfaces.Repositories;
using MembersService.Infrastructure.Common;
using MembersService.Infrastructure.Context;

namespace MembersService.Infrastructure.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}