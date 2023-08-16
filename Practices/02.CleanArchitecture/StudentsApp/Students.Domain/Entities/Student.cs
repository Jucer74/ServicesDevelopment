using Students.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Students.Domain.Entities;

public class Student : EntityBase
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    [Required]
    [RegularExpression(@"[M|F]", ErrorMessage = "Invalid Sex Value")]
    public char Sex { get; set; }
}