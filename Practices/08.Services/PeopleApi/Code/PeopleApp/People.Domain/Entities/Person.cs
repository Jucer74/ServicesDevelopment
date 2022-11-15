using People.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace People.Domain.Entities
{
   public class Person: EntityBase
   {

      [Required]
      public string FirstName { get; set; }

      [Required]
      public string LastName { get; set; }

      public DateTime DateOfBirth { get; set; }

      [Required]
      [RegularExpression(@"[M|F]", ErrorMessage ="Invalid Sex Value")]
      public char Sex { get; set; }
   }
}