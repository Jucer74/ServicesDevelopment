using System.ComponentModel.DataAnnotations;

namespace Pricat.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = string.Empty;
    }
}
