using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;
using System.Linq.Expressions;

namespace Arepas.Application.Interfaces
{
    public interface IProductService
    {

        public Task<IEnumerable<Product>> GetAllAsync();

        public Task<PaginationResult<Product>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Product>> SearchAsync(string queryValue);

        public Task<Product> GetByIdAsync(int id);

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate);

    }
}