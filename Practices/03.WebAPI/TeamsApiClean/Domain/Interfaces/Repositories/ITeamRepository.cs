using Domain.Common;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team?> GetTeamByIdIncludeTeamMember(int id);
    }
}
