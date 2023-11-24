using CategoryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Domain.Interfaces.Proxies
{
    public interface IProductProxy
    {
        public Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId);

        public Task RemoveAsync(int id);

        public Task RemoveRangeAsync(IEnumerable<Product> products);
    }
}
