using System.ComponentModel.DataAnnotations;

namespace TeamsApi.Models;

public class Team
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Coach { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public virtual List<TeamMember> Members { get; set; } = null!;
}