using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Entities.Models
{
    public class Customer : EntityBase
    {
        [Required]
        [EmailAddress(ErrorMessage = " Email erroneo")]

        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [MinimunAge(18)]
        public DateTime? BirthOfDate { get; set; }

        [Required]
        public DateTime? RegisterDate { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }
    }
}
