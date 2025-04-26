using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Users.Domain.Common;

namespace Users.Domain.Entities
{
    public class User : EntityBase
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
