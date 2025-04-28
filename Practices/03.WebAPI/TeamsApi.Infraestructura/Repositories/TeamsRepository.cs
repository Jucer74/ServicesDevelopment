using Microsoft.EntityFrameworkCore;
using TeamsApi.Dominio.Interfaces.Repositories;
using TeamsApi.Dominio.Models;
using TeamsApi.Infraestructura.Common;
using TeamsApi.Infraestructura.Context;

namespace TeamsApi.Infraestructura.Repositories
{
    public class TeamsRepository : Repository<Team>, ITeamRepository
    {
        public TeamsRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Team?> GetTeamByIdIncludeMembers(int id)
        {
            return await _appDbContext.Teams.Include(t => t.Members).Where(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}
