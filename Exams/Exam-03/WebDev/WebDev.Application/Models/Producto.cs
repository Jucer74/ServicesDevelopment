using System.ComponentModel.DataAnnotations;

namespace WebDev.Application.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "El precio es obligatorio")]
        public double Precio { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "La URL de la imagen es obligatoria")]
        public string Imagen { get; set; }
    }

}
