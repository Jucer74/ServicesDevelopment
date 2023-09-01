using NetBank.Domain.Dto;
using NetBank.Domain.Models;

public static class CreditCardMapper
{
    public static IssuingNetworkData MapToDto(IssuingNetwork issuingNetwork)
    {
        return new IssuingNetworkData
        {
            Name = issuingNetwork.Name,
            StartsWithNumbers = issuingNetwork.StartsWithNumbers?.Split(',').Select(int.Parse).ToList(),
            InRange = new RangeNumber
            {
                MinValue = int.Parse(issuingNetwork.InRange.Split('-')[0]),
                MaxValue = int.Parse(issuingNetwork.InRange.Split('-')[1])
            },
            AllowedLengths = issuingNetwork.AllowedLengths.Split(',').Select(int.Parse).ToList()
        };
    }
}
