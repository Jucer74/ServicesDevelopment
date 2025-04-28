using System.ComponentModel.DataAnnotations;
using TeamsApi.Dominio.Common;

namespace TeamsApi.Dominio.Models;

public class Team : EntityBase
{

    [Required]
    public string Name { get; set; } = null!;

    public string Coach { get; set; } = null!;

    public string Conference { get; set; } = null!;

    public virtual List<TeamMember> Members { get; set; } = null!;
}