using UserManagement.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Entities
{
    public class User : EntityBase
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