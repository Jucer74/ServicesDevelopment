using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Entities.Models
{

    public class Order : EntityBase
    {
        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = " El nombre del mensajero es requerido")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public string? DeliveryFirstName { get; set; }

        [Required(ErrorMessage = " El apellido del mensajero es requerido")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public string? DeliveryLastName { get; set; }

        [Required(ErrorMessage = " La direccion del mensajero es requerida")]
        [StringLength(255, ErrorMessage = "La longitud maxima es de 255 caracteres")]
        public string? DeliveryAddress { get; set; }

        [Required(ErrorMessage = " El numero del mensajero es requerido")]
        [StringLength(50, ErrorMessage = "La longitud maxima es de 50 caracteres")]
        public string? DeliveryPhoneNumber { get; set; }

        [StringLength(255, ErrorMessage = "La longitud maxima es de 255 caracteres")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = " El valor total es requerido")]
        public decimal? TotalOrder { get; set; }
    }
}
