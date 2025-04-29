using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Pricat.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        [MaxLength(50, ErrorMessage = "Description's Max Length is 50 Characters")]
        public string Description { get; set; } = null!;

        // Propiedad de navegación
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
