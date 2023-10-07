<<<<<<< HEAD
using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;
using NetBank.Domain.Interfaces.Repositories;
=======
﻿using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;

>>>>>>> 9f758cbdf2457f350595160a18f443a651c27b83

namespace NetBank.Infrastructure.Repositories
{
    public class IssuingNetworkRepository : Repository<IssuingNetwork>, IIssuingNetworkRepository
    {
        public IssuingNetworkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}