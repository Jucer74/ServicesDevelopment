namespace Pricat.Application.Dtos
{
    // Clase que representa el Data Transfer Object (DTO) para la entidad 'Category'.
    // Un DTO es un objeto utilizado para transferir datos entre las capas de la aplicación.
    public class CategoryDto
    {
        // Propiedad que representa el identificador único de la categoría.
        // Es de tipo entero y debe ser único para cada categoría.
        public int Id { get; set; }

        // Propiedad que representa la descripción de la categoría.
        // Es un campo obligatorio que debe contener una cadena de texto.
        // Se inicializa con un valor vacío (string.Empty) para evitar valores nulos.
        public string Description { get; set; } = string.Empty;
    }
}
