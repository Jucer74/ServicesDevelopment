using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities;

public class Person : EntityBase
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [StringLength(1)]
    [RegularExpression("M|F", ErrorMessage = "Sex debe ser 'M' o 'F'.")]
    public string Sex { get; set; } = string.Empty; // True: Masculino, False: Femenino (según convención)
}