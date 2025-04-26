using Application.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.Common;
using TeamsApi.Context;

namespace Infrastructure.Repositories
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
