<<<<<<< HEAD
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

=======
﻿namespace NetBank.Domain.Dto;

public class RangeNumber
{
>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}