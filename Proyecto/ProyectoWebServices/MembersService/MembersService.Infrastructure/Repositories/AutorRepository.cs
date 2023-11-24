using MembersService.Domain.Entities;
using MembersService.Domain.Interfaces;
using MembersService.Infrastructure.Common;
using MembersService.Infrastructure.Context;

namespace MembersService.Infrastructure.Repositories;

public class AutorRepository : Repository<Autor>, IAutorRepository
{
    public AutorRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}