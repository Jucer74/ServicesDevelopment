using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Models;

public class TeamMember : EntityBase
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string Position { get; set; } = null!;

    [ForeignKey("TeamId")]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;
}