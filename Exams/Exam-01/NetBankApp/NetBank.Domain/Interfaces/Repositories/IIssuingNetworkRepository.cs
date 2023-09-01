using NetBank.Domain.Common;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;

namespace NetBank.Domain.Interfaces.Repositories
{
    public interface IIssuingNetworkRepository : IRepository<IssuingNetwork>
    {
        Task<List<IssuingNetworkData>> GetIssuingNetworkDataFromDatabaseAsync();
        Task<List<IssuingNetwork>> GetIssuingNetworksAsync();
    }
}