// Importación de namespaces necesarios
using System.Linq.Expressions;  // Para trabajar con expresiones lambda (como predicados de búsqueda)
using Pricat.Domain.Common;  // Para trabajar con la clase base EntityBase

namespace Pricat.Application.Common
{
    // Definición de la interfaz IRepository, que proporciona operaciones CRUD (Crear, Leer, Actualizar, Eliminar) genéricas
    // Esta interfaz puede ser implementada para cualquier entidad que herede de EntityBase.
    public interface IRepository<T> where T : EntityBase
    {
        // Método para agregar una entidad a la base de datos de forma asíncrona
        Task<T> AddAsync(T entity);

        // Método para obtener todas las entidades de un tipo de forma asíncrona
        Task<IEnumerable<T>> GetAllAsync();

        // Método para obtener una entidad por su ID de forma asíncrona
        Task<T?> GetByIdAsync(int id);

        // Método para buscar entidades que coincidan con un predicado dado (expresión lambda)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // Método para actualizar una entidad en la base de datos de forma asíncrona
        Task<T> UpdateAsync(T entity);

        // Método para eliminar una entidad de la base de datos de forma asíncrona
        Task RemoveAsync(T entity);
    }
}
