using System.ComponentModel.DataAnnotations;

namespace PizzeriaApi.Models;

public class Pizzeria
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Property Type field is required.")]
    [MaxLength(50, ErrorMessage = "Property Type should not exceed 50 characters.")]
    public string Categoriaa { get; set; } = null!;

    public virtual List<PizzeriaCategoria> Categoria { get; set; } = null!;
}
