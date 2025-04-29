using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pricat.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, CategoryId = 1, EanCode = "7707548516286", Description = "Arroz", Unit = "Lb", Price = 500.00m },
            new Product { Id = 2, CategoryId = 1, EanCode = "7707548941507", Description = "Papa", Unit = "Lb", Price = 1500.00m },
            new Product { Id = 3, CategoryId = 2, EanCode = "7707548160274", Description = "Cocacola", Unit = "Und", Price = 2500.00m },
            new Product { Id = 4, CategoryId = 2, EanCode = "7707548110958", Description = "Pepsi", Unit = "und", Price = 2500.00m },
            new Product { Id = 5, CategoryId = 3, EanCode = "7707548758303", Description = "Detergente", Unit = "Kg", Price = 12500.00m },
            new Product { Id = 6, CategoryId = 3, EanCode = "7707548210801", Description = "Cloro", Unit = "CC", Price = 21500.00m },
            new Product { Id = 7, CategoryId = 4, EanCode = "7707548472247", Description = "Camisa", Unit = "Und", Price = 32500.00m },
            new Product { Id = 8, CategoryId = 4, EanCode = "7707548427902", Description = "Pantalon", Unit = "Und", Price = 42500.00m },
            new Product { Id = 9, CategoryId = 5, EanCode = "7707548799412", Description = "Jarabe para la Tos", Unit = "Und", Price = 32500.00m },
            new Product { Id = 10, CategoryId = 5, EanCode = "7707548861546", Description = "Aspirina 500 mg x 20 Unidades", Unit = "Caja", Price = 42500.00m }
        };

        public Task<List<Product>> GetAllAsync() => Task.FromResult(_products);

        public Task<Product?> GetByIdAsync(int id) =>
            Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task<Product> AddAsync(Product product)
        {
            var newId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

            var newProduct = new Product
            {
                Id = newId,
                CategoryId = product.CategoryId,
                EanCode = product.EanCode,
                Description = product.Description,
                Unit = product.Unit,
                Price = product.Price
            };

            _products.Add(newProduct);
            return Task.FromResult(newProduct);
        }

        public Task UpdateAsync(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with Id {id} not found");
            }

            existingProduct.CategoryId = product.CategoryId;
            existingProduct.EanCode = product.EanCode;
            existingProduct.Description = product.Description;
            existingProduct.Unit = product.Unit;
            existingProduct.Price = product.Price;

            return Task.CompletedTask;
        }

        public Task RemoveAsync(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);

            if (productToRemove == null)
            {
                throw new KeyNotFoundException($"Product with Id {id} not found");
            }

            _products.Remove(productToRemove);
            return Task.CompletedTask;
        }


        public Task<List<Product>> GetByCategoryIdAsync(int categoryId)
        {
            var products = _products.Where(p => p.CategoryId == categoryId).ToList();
            return Task.FromResult(products);
        }


        public Task UpdateAsync(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with Id {id} not found");
            }

            existingProduct.CategoryId = product.CategoryId;
            existingProduct.EanCode = product.EanCode;
            existingProduct.Description = product.Description;
            existingProduct.Unit = product.Unit;
            existingProduct.Price = product.Price;

            return Task.CompletedTask;
        }
    }
}