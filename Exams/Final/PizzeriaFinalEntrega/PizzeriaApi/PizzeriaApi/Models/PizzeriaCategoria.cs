using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaApi.Models;

public class PizzeriaCategoria
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "The Nombre field is required.")]
    [MaxLength(50, ErrorMessage = "Nombre should not exceed 50 characters.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "The Tamaño field is required.")]
    [MaxLength(50, ErrorMessage = "Tamaño should not exceed 50 characters.")]
    public string Tamaño { get; set; } = null!;

    [Required(ErrorMessage = "The Precio field is required.")]
    public int Precio { get; set; }

    [ForeignKey("PizzasId")]
    public int PizzasId { get; set; }
    public Pizzeria Pizzas { get; set; } = null!;
}
