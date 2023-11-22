using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaApi.Models;

public class PizzeriaCategoria
{
    [Key]
    public int Id { get; set; }


    [Required(ErrorMessage = "The Address field is required.")]
    [MaxLength(50, ErrorMessage = "Address should not exceed 50 characters.")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "The Location field is required.")]
    [MaxLength(50, ErrorMessage = "Location should not exceed 50 characters.")]
    public string Tamaño { get; set; } = null!;

    [Required(ErrorMessage = "The Price field is required.")]
    public int Precio { get; set; }

    [ForeignKey("PizzasId")]
    public int PizzasId { get; set; }
    public Pizzeria Pizzas { get; set; } = null!;
}
