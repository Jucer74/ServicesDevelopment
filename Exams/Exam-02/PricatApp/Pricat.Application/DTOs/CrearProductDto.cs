using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTOs
{
    public class CrearProductDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string EanCode { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public decimal Price { get; set; }

    }
}