using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Services.Entities
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }

        private ProductDto()
        {

        }

        public static ProductDto Build(int id, string titulo, , double precio, int cantidad, string imagen)
        {
            return new ProductDto
            {
                Id = id,
                Titulo = titulo,
                Precio = precio,
                Cantidad = cantidad,
                Imagen = imagen
            };
        }
    }

}
