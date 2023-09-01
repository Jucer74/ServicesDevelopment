using NetBank.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace NetBank.Domain.Models;

public class IssuingNetwork:EntityBase
{
    public readonly char IssuerPrefix;

    [Required]
    public string Name { get; set; } = null!;

    public string? StartsWithNumbers { get; set; } = null!;

    public string? InRange { get; set; } = null!;

    [Required]
    public string AllowedLengths { get; set; } = null!;
    public object? IssuingNetworkId { get; set; }
    public object? IssuingNetworke { get; set; }
    public object? IssuingNetworkData { get; set; }
}