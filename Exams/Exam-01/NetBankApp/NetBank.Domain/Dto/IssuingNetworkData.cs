namespace NetBank.Domain.Dto;

public class IssuingNetworkData
{
    public string Name { get; set; } = null!;
    public List<int>? StartsWithNumbers { get; set; } = null!;
    public RangeNumber? InRange { get; set; } = null!;
    public List<int> AllowedLengths { get; set; } = null!;
    public string NetworkName { get; set; }
    public IEnumerable<object> NetworkPrefixes { get; set; }
    public int NetworkMinLength { get; set; }
    public int NetworkMaxLength { get; set; }
    public char NetworkPrefix { get; set; }
}