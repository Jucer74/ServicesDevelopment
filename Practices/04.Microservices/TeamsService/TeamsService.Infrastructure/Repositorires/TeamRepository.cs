using TeamsService.Domain.Entities;
using TeamsService.Domain.Interfaces.Repositories;
using TeamsService.Infrastructure.Common;
using TeamsService.Infrastructure.Content;

namespace TeamsService.Infrastructure.Repositorires
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}