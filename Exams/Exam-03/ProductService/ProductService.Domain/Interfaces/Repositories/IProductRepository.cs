using ProductService.Domain.Common;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
