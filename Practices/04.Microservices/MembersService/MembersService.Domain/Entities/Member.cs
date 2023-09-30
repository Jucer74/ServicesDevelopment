using MembersService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MembersService.Domain.Entities;

public class Member : EntityBase
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public int TeamId { get; set; }
}