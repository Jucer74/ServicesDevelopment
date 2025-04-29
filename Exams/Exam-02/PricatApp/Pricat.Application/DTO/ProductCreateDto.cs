using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.DTO
{
    public class ProductCreateDto
    {

        public int CategoryId { get; set; }
        public string? EanCode { get; set; }
        public string? Description { get; set; }
        public string? Unit { get; set; }
        public double Price { get; set; }

    }
}
