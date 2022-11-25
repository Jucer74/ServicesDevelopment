using Arepas.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Arepas.Domain.Entities.Models
{
    public class OrderDetail : EntityBase
    {

        [Required(ErrorMessage = " El Id de la orden es requerido")]
        public int? OrderId { get; set; }

        [Required(ErrorMessage = " El Id del producto es requerido")]
        public int? ProductId { get; set; }

        [Required(ErrorMessage = " El valor es requerido")]
        public int? Quantity { get; set; }


        public decimal? TotalProduct { get; set; }

    }
}

