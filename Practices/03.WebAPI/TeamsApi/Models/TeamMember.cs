using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamsApi.Models;

public class TeamMember
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string Position{ get; set; } = null!;

    [ForeignKey("TeamId")]
    public int TeamId { get; set; }

    public Team Team { get; set; } = null!;
}