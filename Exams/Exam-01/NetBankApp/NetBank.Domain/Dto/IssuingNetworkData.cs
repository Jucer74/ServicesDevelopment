<<<<<<< HEAD
namespace NetBank.Domain.Dto;
=======
﻿namespace NetBank.Domain.Dto;
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83

public class IssuingNetworkData
{
    public string Name { get; set; } = null!;
    public List<int>? StartsWithNumbers { get; set; } = null!;
    public RangeNumber? InRange { get; set; } = null!;
    public List<int> AllowedLengths { get; set; } = null!;
<<<<<<< HEAD
    public int Id { get; set; }
=======
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
}