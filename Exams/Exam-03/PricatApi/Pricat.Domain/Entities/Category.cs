using Pricat.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Entities
{
   public class Category : EntityBase
   {
      [Required(ErrorMessage = "Description is Required")]
      [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
      public string Description { get; set; }
   }
}