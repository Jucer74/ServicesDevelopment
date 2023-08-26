using NetBank.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Models;

public class IssuingNetwork:EntityBase
{
    [Required]
    public string Name { get; set; } = null!;

    public string? StartsWithNumbers { get; set; } = null!;

    public string? InRange { get; set; } = null!;

    [Required]
    public string AllowedLengths { get; set; } = null!;
}