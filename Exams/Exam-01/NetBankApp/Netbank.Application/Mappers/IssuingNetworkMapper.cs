using Netbank.Application.Utils;
using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netbank.Application.Mappers
{
    public class IssuingNetworkMapper
    {
        public IssuingNetworkData ToIssuingNetworkData(IssuingNetwork issuignNetwork)
        {
            IssuingNetworkData issuingNetworkData = new();
            issuingNetworkData.Name = issuignNetwork.Name;
            issuingNetworkData.StartsWithNumbers = StringTransformer.ComaSeparatedValuesToIntList(issuignNetwork.StartsWithNumbers);
            issuingNetworkData.InRange = StringTransformer.HyphenSeparatedValuesToRangeNumber(issuignNetwork.InRange);
            issuingNetworkData.AllowedLengths = StringTransformer.ComaSeparatedValuesToIntList(issuignNetwork.AllowedLengths);
            return issuingNetworkData;

        }


    }
}
