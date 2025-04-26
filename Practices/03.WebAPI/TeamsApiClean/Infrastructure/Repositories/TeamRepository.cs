using Domain.Models;
using Application.Interfaces.Repositories;
using Infrastructure.Common;
using TeamsApi.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Team?> GetTeamByIdIncludeTeamMember(int id)
        {
            return await _appDbContext.Teams.Include(m => m.Members).Where(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}
