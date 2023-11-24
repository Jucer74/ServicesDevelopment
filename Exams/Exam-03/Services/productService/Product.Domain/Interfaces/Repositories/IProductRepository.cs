using Product.Domain.Common;
using Product.Domain.Entities;

namespace Product.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<EProduct>
    {
    }
}