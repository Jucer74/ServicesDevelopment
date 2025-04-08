using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities;

public class User : EntitiyBase
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string UserName { get; set; }
}
