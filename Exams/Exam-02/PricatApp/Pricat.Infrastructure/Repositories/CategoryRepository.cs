// Importación de los namespaces necesarios
using Pricat.Application.Interfaces.Repositories;  // Para acceder a las interfaces de repositorios
using Pricat.Domain.Models;  // Para acceder al modelo Category
using Pricat.Infrastructure.Common;  // Para acceder a la clase base Repository
using Pricat.Infrastructure.Context;  // Para acceder al contexto de base de datos

namespace Pricat.Infrastructure.Repositories
{
    // Clase que implementa el repositorio de la entidad Category, heredando de Repository<Category>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        // Constructor que recibe el contexto de la base de datos y lo pasa al constructor base
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
