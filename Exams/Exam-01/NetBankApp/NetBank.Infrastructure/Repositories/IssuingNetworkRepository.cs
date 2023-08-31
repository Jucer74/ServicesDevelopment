using Microsoft.EntityFrameworkCore;
using NetBank.Domain.Interfaces.Repositories;
using NetBank.Domain.Models;
using NetBank.Infrastructure.Common;
using NetBank.Infrastructure.Context;
using System;


namespace NetBank.Infrastructure.Repositories
{
    public class IssuingNetworkRepository : Repository<IssuingNetwork>, IIssuingNetworkRepository
    {
        private readonly AppDbContext _appDbContext;

        public IssuingNetworkRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public async Task<IEnumerable<IssuingNetwork>> getAll()
        {
            return await _appDbContext.Set<IssuingNetwork>().ToListAsync<IssuingNetwork>();
        }
    }
}