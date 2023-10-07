using System.ComponentModel.DataAnnotations;
using TeamsService.Domain.Common;

namespace TeamsService.Domain.Entities;

public class Team: EntityBase
{
    [Required(ErrorMessage = "The Team Name is required.")]
    [StringLength(50, ErrorMessage = "The maximum length of Team Name is 50 characters.")]
    public string Name { get; set; } = null!;

    [StringLength(50, ErrorMessage = "The maximum length of Coach Name is 50 characters.")]
    public string Coach { get; set; } = null!;

    [StringLength(50, ErrorMessage = "The maximum length of Conference is 20 characters.")]
    public string Conference { get; set; } = null!;
}