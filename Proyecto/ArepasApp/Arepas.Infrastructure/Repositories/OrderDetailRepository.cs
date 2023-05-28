﻿using Arepas.Domain.Interfaces.Repositories;
using Arepas.Domain.Models;
using Arepas.Infrastructure.Common;
using Arepas.Infrastructure.Context;

namespace Arepas.Infrastructure.Repositories;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    public OrderDetailRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}