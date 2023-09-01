using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Domain.Dto;

[NotMapped]
public class RangeNumber
{
    public RangeNumber(int minValue, int maxValue)
    {
        MinValue = minValue;
        MaxValue = maxValue;
    }

    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}