using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities;

public class User : EntityBase
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required ]
    public required string FullName { get; set; }

    [Required]
    public required string Password { get; set; }

    [Required]
    public required string UserName { get; set; }
}