using TeamsService.Domain.Common;
using TeamsService.Domain.Entities;

namespace MembersService.Domain.Interfaces.Repositories
{
    public interface IMemberRepository : IRepository<Team>
    {
    }
}