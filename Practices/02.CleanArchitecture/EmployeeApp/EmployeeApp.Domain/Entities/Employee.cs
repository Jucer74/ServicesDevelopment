using EmployeeApp.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Domain.Entities
{
   public class Employee : EntityBase
   {
      [Required]
      public string? FirstName { get; set; }

      [Required]
      public string? LastName { get; set; }

      public DateTime HireDate { get; set; }

      [Required]
      public string? Department { get; set; }
   }
}