using NetBank.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetBank.Domain.Dto;

namespace NetBank.Domain.Models;

[Table("issuingnetworks")]
public class IssuingNetwork:EntityBase
{   
    [Required]
    public string Name { get; set; } = null!;

    public string? StartsWithNumbers { get; set; } = null!;

    public string? InRange { get; set; } = null!;

    [Required]
    public string AllowedLengths { get; set; } = null!;
}