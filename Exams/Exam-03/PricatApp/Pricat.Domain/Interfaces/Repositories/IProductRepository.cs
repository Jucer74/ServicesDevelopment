using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pricat.Domain.Common;
using Pricat.Domain.Entities;

namespace Pricat.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId);

        public Task RemoveAllByCategoryIdAsync(int categoryId);

    }


}