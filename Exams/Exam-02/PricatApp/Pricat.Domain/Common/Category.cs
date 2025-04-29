using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Domain.Common
{
    public class Category
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        // Relación: Una categoría puede tener muchos productos
        public ICollection<Product>? Products { get; set; }
    }

}
