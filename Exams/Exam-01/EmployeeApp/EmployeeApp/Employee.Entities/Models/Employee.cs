using System.ComponentModel.DataAnnotations;

namespace Employee.Entities.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        [Required]
        public string Department { get; set; }
    }
}