using Pricat.Application.Interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _reminderRepository;

        public ProductService(IProductRepository reminderRepository)
        {
            _reminderRepository = reminderRepository;
        }

        public async Task AddAsync(Product entity)
        {
            await _reminderRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _reminderRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Product>> FindIdAsync(int id)
        {
            return await _reminderRepository.FindIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _reminderRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var person = await _reminderRepository.GetByIdAsync(id);

            // Validte If Exist
            return person;
        }

        public async Task RemoveAsync(int id)
        {
            var person = await _reminderRepository.GetByIdAsync(id);
            await _reminderRepository.RemoveAsync(person);
        }

        public async Task UpdateAsync(int id, Product entity)
        {
            // Validate if Exist
            await _reminderRepository.UpdateAsync(entity);
        }
    }
}
