using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;

namespace NetBank.Infrastructure.Repositories
{
    public class IssuingNetworkRepository : Repository<IssuingNetwork>, IIssuingNetworkRepository
    {
        public IssuingNetworkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}