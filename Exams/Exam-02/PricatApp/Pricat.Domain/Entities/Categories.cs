using System.ComponentModel.DataAnnotations;
using Pricat.Domain.Common;

namespace Pricat.Domain.Entities
{
    public class Categories : EntityBase
    {
        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string Description { get; set; } = null!;
    }
}
