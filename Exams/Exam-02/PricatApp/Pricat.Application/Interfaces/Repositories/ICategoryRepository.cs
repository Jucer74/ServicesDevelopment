using Pricat.Application.Common;
using Pricat.Domain.Models;

namespace Pricat.Application.Interfaces.Repositories
{
    // Interfaz que extiende la funcionalidad del repositorio base para la entidad Category.
    // Define los métodos específicos para interactuar con la entidad Category en la base de datos.
    // Hereda de IRepository<Category>, lo que permite operaciones generales como AddAsync, GetByIdAsync, etc.
    public interface ICategoryRepository : IRepository<Category>
    {
        // Aquí se pueden agregar métodos específicos para la entidad Category si es necesario.
    }
}
