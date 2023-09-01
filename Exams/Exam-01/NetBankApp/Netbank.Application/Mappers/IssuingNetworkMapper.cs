using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using static NetBank.Domain.Dto.IssuingNetworkData;

namespace Netbank.Application.Mappers
{
    public class IssuingNetworkMapper
    {
        public static IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuignNetwork)
        {
            IssuingNetworkData issuingNetworkData = new();
            issuingNetworkData.Name = issuignNetwork.Name;
            issuingNetworkData.StartsWithNumbers = Convertidor.ConvertCommaSeparatedToIntList(issuignNetwork.StartsWithNumbers);
            issuingNetworkData.InRange = Convertidor.GuionRangeConverter(issuignNetwork.InRange);
            issuingNetworkData.AllowedLengths = Convertidor.ConvertCommaSeparatedToIntList(issuignNetwork.AllowedLengths);
            return issuingNetworkData;

        }

        public static List<IssuingNetworkData> ToIssuingNetworkDataList(List<IssuingNetwork> issuingNetworks)
        {
            List<IssuingNetworkData> issuingNetworkDataList = new();
            foreach (IssuingNetwork issuingNetwork in issuingNetworks)
            {
                IssuingNetworkData issuignNetworkData = ToIssuingNetworkData(issuingNetwork);
                issuingNetworkDataList.Add(issuignNetworkData);
            }
            return issuingNetworkDataList;
        }


    }

}
