using NetBank.Domain.Dto;
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

        Task<List<IssuingNetworkData>> IIssuingNetworkRepository.GetIssuingNetworkDataFromDatabaseAsync()
        {
            throw new NotImplementedException();
        }

        Task<List<IssuingNetwork>> IIssuingNetworkRepository.GetIssuingNetworksAsync()
        {
            throw new NotImplementedException();
        }
    }
}