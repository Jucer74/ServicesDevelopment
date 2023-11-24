using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Interfaces.Proxies
{
    public interface ICategoryProxy
    {
        public Task<Category> GetByIdAsync(int id);
    }
}
