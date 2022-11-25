using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arepas.Domain.Entities.Dtos
{
    public class OrderDetailDto
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public float? Quantity { get; set; }

        public float? TotalProduct { get; set; }

    }
}
