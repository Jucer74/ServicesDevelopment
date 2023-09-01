using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Netbank.Application.Mappers
{
    public static class IssuingNetworkDataMapper
    {
        public static List<IssuingNetworkData> MapIssuingNetworksToData(List<IssuingNetwork> issuingNetworks)
        {
            List<IssuingNetworkData> issuingNetworkDataList = new();

            foreach (IssuingNetwork issuingNetwork in issuingNetworks)
            {
                IssuingNetworkData issuingNetworkData = MapIssuingNetworkToData(issuingNetwork);
                issuingNetworkDataList.Add(issuingNetworkData);
            }

            return issuingNetworkDataList;
        }

        public static IssuingNetworkData MapIssuingNetworkToData(IssuingNetwork issuingNetwork)
        {
            return new IssuingNetworkData
            {
                Name = issuingNetwork.Name,
                StartsWithNumbers = ConvertNumbersToList(issuingNetwork.StartsWithNumbers),
                InRange = CreateRangeNumberObject(issuingNetwork.InRange),
                AllowedLengths = ConvertNumbersToList(issuingNetwork.AllowedLengths)
            };
        }

        private static List<int> ConvertNumbersToList(string? numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return new List<int>();
            }

            List<string> numbersStringList = numbers.Split(',').Select(num => num.Trim()).ToList();
            List<int> numbersIntList = numbersStringList.Select(int.Parse).ToList();

            return numbersIntList;
        }

        private static RangeNumber? CreateRangeNumberObject(string? inRange)
        {
            if (string.IsNullOrEmpty(inRange))
            {
                return null;
            }

            string[] rangeParts = inRange.Trim().Split('-');

            if (rangeParts.Length != 2)
            {
                throw new ArgumentException("Invalid range format.");
            }

            return new RangeNumber
            {
                MinValue = int.Parse(rangeParts[0]),
                MaxValue = int.Parse(rangeParts[1])
            };
        }
    }
}
