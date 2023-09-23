using NetBank.Domain.Dto;
using NetBank.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netbank.Application.Mappers
{
    public static class IssuingNetworkDataMapper
    {
        public static List<IssuingNetworkData> MapsFromIssuingNetworkToIssuingNetworkData (List<IssuingNetwork> issuingNetworks)
        {
            List<IssuingNetworkData> issuingNetworkDataList = new();

            foreach (IssuingNetwork issuingNetwork in issuingNetworks)
            {
                IssuingNetworkData issuingNetworkData = new()
                {
                    Name = issuingNetwork.Name,
                    StartsWithNumbers = ConvertNumbersToList(issuingNetwork.StartsWithNumbers),
                    InRange = CreateRangeNumberObject(issuingNetwork.InRange),
                    AllowedLengths = ConvertNumbersToList(issuingNetwork.AllowedLengths)
                };

                issuingNetworkDataList.Add(issuingNetworkData);
            }

            return issuingNetworkDataList;
        }

        private static List<int> ConvertNumbersToList(string? numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return new List<int>();
            }

            //Creates a list of strings using the Split method
            List<string> numbersStringList = numbers.Split(",").Select(num => num.Trim()).ToList();

            //Parses every string of the list to an integer
            List<int> numbersIntList = numbersStringList.Select(int.Parse).ToList();

            return numbersIntList;
        }

        private static RangeNumber? CreateRangeNumberObject(string? inRange)
        {
            RangeNumber rangeNumber = new();

            if (string.IsNullOrEmpty(inRange))
            {
                return null;
            }

            //Separates the minimun and maximun number by the '-' char
            string[] rangeParts = inRange.Trim().Split('-');

            rangeNumber.MinValue = int.Parse(rangeParts[0]);
            rangeNumber.MaxValue = int.Parse(rangeParts[1]);

            return rangeNumber;

        }
    }
}
