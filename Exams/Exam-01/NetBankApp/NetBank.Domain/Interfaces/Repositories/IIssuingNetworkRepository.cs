using NetBank.Domain.Common;
using NetBank.Domain.Models;

namespace NetBank.Domain.Interfaces.Repositories
{
    public interface IIssuingNetworkRepository : IRepository<IssuingNetwork>
    {
        List<IssuingNetwork> GetAllIssuingNetworks();


    }

}
