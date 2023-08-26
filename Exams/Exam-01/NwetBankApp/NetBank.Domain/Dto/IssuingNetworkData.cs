namespace NetBank.Domain.Dto;

public class IssuingNetworkData
{
    public string Name { get; set; } = null!;
    public List<int>? StartsWithNumbers { get; set; } = null!;
    public RangeNumber? InRange { get; set; } = null!;
    public List<int> AllowedLengths { get; set; } = null!;
}