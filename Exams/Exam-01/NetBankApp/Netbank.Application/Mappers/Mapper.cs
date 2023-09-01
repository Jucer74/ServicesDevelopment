using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System.Collections.Generic;
using static NetBank.Domain.Dto.IssuingNetworkData;

namespace Netbank.Application.Mappers
{
    public static class Mapper
    {
        public static IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuignNetwork)
        {
            IssuingNetworkData issuingNetworkData = new();
            issuingNetworkData.Name = issuignNetwork.Name;

            if (issuignNetwork.StartsWithNumbers != null)
            {
                issuingNetworkData.StartsWithNumbers = Convertidor.ConvertCommaSeparatedToIntList(issuignNetwork.StartsWithNumbers);
            }

            if (issuignNetwork.InRange != null)
            {
                issuingNetworkData.InRange = Convertidor.GuionRangeConverter(issuignNetwork.InRange);
            }

            if (issuignNetwork.AllowedLengths != null)
            {
                issuingNetworkData.AllowedLengths = Convertidor.ConvertCommaSeparatedToIntList(issuignNetwork.AllowedLengths!);
            }


            return issuingNetworkData;
        }

        public static List<IssuingNetworkData> ToIssuingNetworkDataList(List<IssuingNetwork> issuingNetworks)
        {
            List<IssuingNetworkData> issuingNetworkDataList = new();
            foreach (IssuingNetwork issuingNetwork in issuingNetworks)
            {
                if (issuingNetwork != null)
                {
                    IssuingNetworkData issuingNetworkData = ToIssuingNetworkData(issuingNetwork);
                    issuingNetworkDataList.Add(issuingNetworkData);
                }
            }
            return issuingNetworkDataList;
        }
    }
}
