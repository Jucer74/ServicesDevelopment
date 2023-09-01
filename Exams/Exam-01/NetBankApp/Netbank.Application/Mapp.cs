using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System.Collections.Generic;
using static NetBank.Domain.Dto.IssuingNetworkData;

namespace Netbank.Application.Map
{
    public static class IMapp
    {
        public static IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuignNetwork)
        {
            IssuingNetworkData issuingNetworkData = new();
            issuingNetworkData.Name = issuignNetwork.Name;

            if (issuignNetwork.StartsWithNumbers != null)
            {
                issuingNetworkData.StartsWithNumbers = Alter.AlterCommaIntoaList(issuignNetwork.StartsWithNumbers);
            }

            if (issuignNetwork.InRange != null)
            {
                issuingNetworkData.InRange = Alter.ChangeGuionRange(issuignNetwork.InRange);
            }

            if (issuignNetwork.AllowedLengths != null)
            {
                issuingNetworkData.AllowedLengths = Alter.AlterCommaIntoaList(issuignNetwork.AllowedLengths!);
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
