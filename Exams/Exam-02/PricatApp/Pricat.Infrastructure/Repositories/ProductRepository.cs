using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pricat.Domain.Common;
using Pricat.Domain.Entities;
using Pricat.Infrastructure.Persistence.Context;

namespace Pricat.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PricatDbContext _context;

        public ProductRepository(PricatDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product); // ← AQUÍ está el cambio
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            // Busca el producto existente en la base de datos
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (existingProduct != null)
            {
                // Actualiza las propiedades del producto existente
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.EanCode = product.EanCode;
                existingProduct.Description = product.Description;
                existingProduct.Unit = product.Unit;
                existingProduct.Price = product.Price;

                // Guarda los cambios en la base de datos
                await _context.SaveChangesAsync();
            }
            else
            {
                // Si no se encuentra el producto, podrías manejar el error o retornar algún valor
                throw new KeyNotFoundException("El producto con el ID proporcionado no se encontró.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

    }
}