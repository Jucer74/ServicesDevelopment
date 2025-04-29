// Importación de los namespaces necesarios
using Pricat.Application.Interfaces.Repositories;  // Para acceder a las interfaces de repositorios
using Pricat.Domain.Models;  // Para acceder al modelo Product
using Pricat.Infrastructure.Common;  // Para acceder a la clase base Repository
using Pricat.Infrastructure.Context;  // Para acceder al contexto de base de datos
using Microsoft.EntityFrameworkCore;  // Para usar las funcionalidades de EF Core como ToListAsync y FirstOrDefaultAsync

namespace Pricat.Infrastructure.Repositories
{
    // Clase que implementa el repositorio de la entidad Product, heredando de Repository<Product>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        // Constructor que recibe el contexto de la base de datos y lo pasa al constructor base
        public ProductRepository(AppDbContext context) : base(context) { }

        // Método que obtiene los productos por categoría (basado en el CategoryId)
        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            // Filtra los productos que pertenecen a una categoría específica y devuelve la lista
            return await _appDbContext.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();  // Convierte los resultados en una lista asincrónica
        }

        // Método que obtiene un producto por su código EAN (número de identificación único)
        public async Task<Product?> GetByEanCodeAsync(string eanCode)
        {
            // Busca el primer producto que tenga el mismo código EAN, o retorna null si no lo encuentra
            return await _appDbContext.Products
                .FirstOrDefaultAsync(p => p.EanCode == eanCode);
        }
    }
}
