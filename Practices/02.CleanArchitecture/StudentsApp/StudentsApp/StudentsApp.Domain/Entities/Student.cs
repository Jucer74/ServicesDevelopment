using StudentsApp.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace StudentsApp.Domain.Entities
{
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
}
