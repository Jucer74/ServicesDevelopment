using Arepas.Domain.Common;
using Arepas.Domain.Entities.Dto;
using Arepas.Domain.Entities.Models;

namespace Arepas.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<PaginationResult<Product>> GetByPageAsync(PaginationParams @params);

        public Task<IEnumerable<Product>> SearchAsync(string queryValue);
    }
}
