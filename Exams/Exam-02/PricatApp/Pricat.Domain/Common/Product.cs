using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Domain.Common
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string EanCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Relación: Producto pertenece a una categoría
        public Category? Category { get; set; }
    }
}
