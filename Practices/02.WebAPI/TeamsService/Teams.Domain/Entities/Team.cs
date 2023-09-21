using System.ComponentModel.DataAnnotations;
using Teams.Domain.Common;

namespace Teams.Domain.Entities;

public class Team : EntityBase
{
    [Required]
    public string Name { get; set; } = null!;
    public string Coach { get; set; } = null!;
    public string Conference { get; set; } = null!;

}