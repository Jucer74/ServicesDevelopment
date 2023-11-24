using System.ComponentModel.DataAnnotations;

namespace PizzeriaApi.Models;

public class Pizzeria
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The Categoriaa field is required.")]
    [MaxLength(50, ErrorMessage = "Categoriaa should not exceed 50 characters.")]
    public string Categoriaa { get; set; } = null!;

    public virtual List<PizzeriaCategoria> Categoria { get; set; } = null!;
}
