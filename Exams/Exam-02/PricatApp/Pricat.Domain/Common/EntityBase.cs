// Importación de los namespaces necesarios
using System.ComponentModel.DataAnnotations;  // Para usar atributos de validación de datos como [Key]

namespace Pricat.Domain.Common
{
    // Clase base que contiene propiedades comunes para todas las entidades
    public class EntityBase
    {
        // Propiedad que representa el identificador único de la entidad
        // El atributo [Key] indica que esta propiedad es la clave primaria en la base de datos
        [Key]
        public int Id { get; set; }
    }
}
