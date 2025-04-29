namespace Pricat.Application.Dtos
{
    // Clase que representa el Data Transfer Object (DTO) para la entidad 'Product'.
    public class ProductDto
    {
        // Propiedad que representa el identificador único del producto.
        // Es de tipo entero y debe ser único para cada producto.
        public int Id { get; set; }

        // Propiedad que representa el identificador de la categoría a la que pertenece el producto.
        // Es de tipo entero y se usa para asociar un producto con una categoría específica.
        public int CategoryId { get; set; }

        // Propiedad que representa el código EAN del producto.
        // Es una cadena de texto con una longitud máxima de 13 caracteres.
        // El código EAN es un identificador único para productos a nivel global.
        public string EanCode { get; set; } = string.Empty;

        // Propiedad que representa la descripción del producto.
        // Es una cadena de texto que describe el producto de forma breve.
        public string Description { get; set; } = string.Empty;

        // Propiedad que representa la unidad de medida del producto (por ejemplo, "kg", "pieza").
        // Es una cadena de texto que describe la unidad de venta del producto.
        public string Unit { get; set; } = string.Empty;

        // Propiedad que representa el precio del producto.
        // Es de tipo decimal y define el precio de venta del producto.
        // El precio es un número con 2 decimales.
        public decimal Price { get; set; }
    }
}
