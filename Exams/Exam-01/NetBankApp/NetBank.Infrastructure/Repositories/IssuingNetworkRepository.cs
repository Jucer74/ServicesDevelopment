using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;

namespace NetBank.Infrastructure.Repositories
{
    public class IssuingNetworkRepository : Repository<IssuingNetwork>, IIssuingNetworkRepository
    {
        public IssuingNetworkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<IssuingNetwork> GetAllIssuingNetworks()
        {
            return _appDbContext.IssuingNetworks.ToList();
        }
    }
}
