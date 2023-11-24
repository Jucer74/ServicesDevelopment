using CategoryService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Domain.Entities;

   public class Category : EntityBase
   {
    [Required(ErrorMessage = "The Description   is required.")]
    [StringLength(50, ErrorMessage = "The maximum length of Description is 50 characters.")]
    public string Description { get; set; } = null!;
   }
