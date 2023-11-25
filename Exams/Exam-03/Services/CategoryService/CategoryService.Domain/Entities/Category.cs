using CategoryService.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Domain.Entities
{
    public class Category : EntityBase
    {
        private string? description;

        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string? Description { get => description; set => description = value; }
    }
}