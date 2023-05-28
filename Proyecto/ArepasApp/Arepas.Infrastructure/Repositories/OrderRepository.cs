using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;

namespace Arepas.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}