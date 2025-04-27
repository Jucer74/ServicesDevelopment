using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class User : EntityBase
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string UserName { get; set; }
    }
}
