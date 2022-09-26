using System.ComponentModel.DataAnnotations;

namespace Person.Entities.Models
{
   public class Person
   {
      [Key]
      public int Id { get; set; }

      [Required]
      public string FirstName { get; set; }

      [Required]
      public string LastName { get; set; }

      public DateTime DateOfBirth { get; set; }

      [Required]
      public char Sex { get; set; }
   }
}