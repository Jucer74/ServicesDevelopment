using TeamsService.Domain.Entities;
using TeamsService.Domain.Interfaces.Repositories;
using TeamsService.Infrastructure.Common;
using TeamsService.Infrastructure.Context;

namespace MembersService.Infrastructure.Repositories
{
    public class MemberRepository : Repository<Team>, ITeamRepository
    {
        public MemberRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}