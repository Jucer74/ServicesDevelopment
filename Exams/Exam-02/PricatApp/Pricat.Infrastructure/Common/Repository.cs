// Importación de los namespaces necesarios
using Microsoft.EntityFrameworkCore;  // Para usar Entity Framework Core
using System.Linq.Expressions;  // Para trabajar con expresiones lambda en las consultas
using Pricat.Application.Common;  // Namespace donde se encuentran las interfaces comunes
using Pricat.Infrastructure.Context;  // Namespace con el contexto de la base de datos
using Pricat.Domain.Common;  // Namespace con las clases base del dominio
using Pricat.Application.Exceptions;  // Namespace con las excepciones personalizadas

namespace Pricat.Infrastructure.Common
{
    // Clase genérica Repository que implementa la interfaz IRepository para trabajar con entidades del tipo T
    // T debe ser una clase que herede de EntityBase
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        // Contexto de la base de datos que se inyecta en el constructor
        protected readonly AppDbContext _appDbContext;

        // Constructor que recibe el contexto de la base de datos y lo asigna al campo _appDbContext
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Método asincrónico para agregar una nueva entidad en la base de datos
        public async Task<T> AddAsync(T entity)
        {
            // Se agrega la entidad a la tabla correspondiente
            _appDbContext.Set<T>().Add(entity);

            // Se guardan los cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Se retorna la entidad agregada
            return entity;
        }

        // Método asincrónico para buscar entidades que coincidan con un predicado (expresión lambda)
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // Se filtran las entidades según el predicado y se retorna la lista de resultados
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
        }

        // Método asincrónico para obtener todas las entidades de la base de datos
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Se obtiene y retorna todas las entidades de la tabla correspondiente
            return await _appDbContext.Set<T>().ToListAsync();
        }

        // Método asincrónico para obtener una entidad por su identificador (ID)
        public async Task<T?> GetByIdAsync(int id)
        {
            // Se busca la entidad por su ID y se retorna
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        // Método asincrónico para eliminar una entidad de la base de datos
        public async Task RemoveAsync(T entity)
        {
            // Se elimina la entidad de la tabla correspondiente
            _appDbContext.Set<T>().Remove(entity);

            // Se guardan los cambios en la base de datos
            await _appDbContext.SaveChangesAsync();
        }

        // Método asincrónico para actualizar una entidad en la base de datos
        public async Task<T> UpdateAsync(T entity)
        {
            var id = entity?.Id;  // Se obtiene el ID de la entidad
            var original = await _appDbContext.Set<T>().FindAsync(id);  // Se busca la entidad original por su ID

            // Si no se encuentra la entidad, se lanza una excepción personalizada
            if (original is null)
            {
                throw new NotFoundException($"Entity with Id={id} Not Found");
            }

            // Se actualizan los valores de la entidad original con los de la entidad proporcionada
            _appDbContext.Entry(original).CurrentValues.SetValues(entity!);

            // Se guardan los cambios en la base de datos
            await _appDbContext.SaveChangesAsync();

            // Se retorna la entidad actualizada
            return entity!;
        }
    }
}
