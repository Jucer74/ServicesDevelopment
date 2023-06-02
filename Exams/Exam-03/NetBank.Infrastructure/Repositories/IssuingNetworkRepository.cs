using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;
using Pricat.Domain.Interfaces.Repositories;

namespace NetBank.Infrastructure.Repositories
{
    public class IssuingNetworkRepository : Repository<IssuingNetwork>, IIssuingNetworkRepository
    {
        public IssuingNetworkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}