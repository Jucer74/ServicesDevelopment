namespace NetBank.Domain.Dto;

public class IssuingNetworkData
{
    public string? Name { get; set; } 
    public List<int>? StartsWithNumbers { get; set; } 
    public RangeNumber? InRange { get; set; } 
    public List<int>? AllowedLengths { get; set; }
}