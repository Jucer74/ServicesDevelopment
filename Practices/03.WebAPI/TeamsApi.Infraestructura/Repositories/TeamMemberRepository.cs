using TeamsApi.Dominio.Interfaces.Repositories;
using TeamsApi.Dominio.Models;
using TeamsApi.Infraestructura.Common;
using TeamsApi.Infraestructura.Context;

namespace TeamsApi.Infraestructura.Repositories
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
