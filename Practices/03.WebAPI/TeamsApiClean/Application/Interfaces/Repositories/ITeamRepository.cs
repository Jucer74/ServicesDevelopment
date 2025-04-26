using Application.Common;
using Domain.Models;

namespace Application.Interfaces.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<Team?> GetTeamByIdIncludeTeamMember(int id);
    }
}
