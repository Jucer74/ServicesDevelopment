using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTOs
{
    public class ProductCreateDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public required string EanCode { get; set; }
        public required string Description { get; set; }
        public required string Unit { get; set; }
        public decimal Price { get; set; }
    }
}

