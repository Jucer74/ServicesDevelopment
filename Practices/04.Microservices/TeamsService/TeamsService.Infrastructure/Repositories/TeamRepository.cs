using TeamsService.Domain.Entities;
using TeamsService.Domain.Interfaces.Repositories;
using TeamsService.Infrastructure.Common;
using TeamsService.Infrastructure.Context;

namespace TeamsService.Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}