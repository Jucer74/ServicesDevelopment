using System.ComponentModel.DataAnnotations;
using UserManagement.Domain.Common;

namespace UserManagement.Domain.Entities
{
    public class User : EntityBase
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }
        public User()
        {
            Email = "";
            FullName = "";
            Password = "";
            UserName = "";
        }
    }
}