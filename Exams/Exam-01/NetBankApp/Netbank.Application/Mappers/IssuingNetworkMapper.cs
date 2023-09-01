using NetBank.Domain;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netbank.Application.Mappers
{
    public class CreditCardMapper
    {
        public static IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuignNetwork)
        {
            IssuingNetworkData issuingNetworkData = new();
            issuingNetworkData.Name = issuignNetwork.Name;
            issuingNetworkData.StartsWithNumbers = StringTransformer.ComaSeparatedValuesToIntList(issuignNetwork.StartsWithNumbers);
            issuingNetworkData.InRange = StringTransformer.HyphenSeparatedValuesToRangeNumber(issuignNetwork.InRange);
            issuingNetworkData.AllowedLengths = StringTransformer.ComaSeparatedValuesToIntList(issuignNetwork.AllowedLengths);
            return issuingNetworkData;

        }

        public static List<IssuingNetworkData> ToIssuingNetworkDataList(List<IssuingNetwork> issuingNetworks) {
            List<IssuingNetworkData> issuingNetworkDataList = new();
            foreach(IssuingNetwork issuingNetwork in issuingNetworks)
            {
                IssuingNetworkData issuignNetworkData = ToIssuingNetworkData(issuingNetwork);
                issuingNetworkDataList.Add(issuignNetworkData);
            }
            return issuingNetworkDataList;
        }
    }
}
