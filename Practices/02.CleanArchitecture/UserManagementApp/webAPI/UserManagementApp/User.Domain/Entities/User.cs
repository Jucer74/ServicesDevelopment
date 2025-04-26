using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Users.Domain.Common;

namespace Users.Domain.Entities
{
    public class User : EntityBase
    {
        public required string Email { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        [PasswordPropertyText]
        public required string Password { get; set; }
        [Required]
        public required string UserName { get; set; }
    }
}
