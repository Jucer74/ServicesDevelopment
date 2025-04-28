using TeamsApi.Dominio.Common;
using TeamsApi.Dominio.Models;

namespace TeamsApi.Dominio.Interfaces.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team?> GetTeamByIdIncludeMembers(int id); 
    }
}
