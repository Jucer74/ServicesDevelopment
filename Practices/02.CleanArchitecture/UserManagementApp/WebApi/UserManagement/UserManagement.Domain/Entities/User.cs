using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Common;
namespace UserManagement.Domain.Entities;

public class User : EntityBase
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string Fullname { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string Username { get; set; } = null!;
}

