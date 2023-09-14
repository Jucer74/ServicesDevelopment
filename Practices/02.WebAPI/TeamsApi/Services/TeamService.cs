using Microsoft.EntityFrameworkCore;
using TeamsApi.Context;
using TeamsApi.Models;

namespace TeamsApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _appDbContext;

        public TeamService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Team> CreateTeam(Team team)
        {
            _appDbContext.Set<Team>().Add(team);
            await _appDbContext.SaveChangesAsync();
            return team; 
        }

        public async Task DeleteTeam(int id)
        {
            var original = await _appDbContext.Set<Team>().FindAsync(id);

            if (original is null)
            {
                throw new Exception($"Team with Id={id} Not Found");
            }

            _appDbContext.Set<Team>().Remove(original);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Team>> GetAllTeams()
        {
            return await _appDbContext.Set<Team>().ToListAsync<Team>();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return (await _appDbContext.Set<Team>().FindAsync(id))!;
        }

        public async Task<List<TeamMember>> GetTeamMembersByTeamId(int id)
        {
            var teamMembers = await _appDbContext.Teams
                                    .Include(m => m.Members)
                                    .Where(t => t.Id == id)
                                    .FirstOrDefaultAsync();
                                    

            return teamMembers!.Members;
        }

        public async Task<Team> UpdateTeam(int id, Team team)
        {
            var original = await _appDbContext.Set<Team>().FindAsync(id);

            if (original is null)
            {
                throw new Exception($"Team with Id={id} Not Found");
            }

            _appDbContext.Entry(original).CurrentValues.SetValues(team!);
            await _appDbContext.SaveChangesAsync();

            return team!;
        }
    }
}
