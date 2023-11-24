
using TeamsService.Domain.Interfaces.Repositories;
using TeamsService.Infrastructure.Common;
using TeamsService.Infrastructure.Context;
using TeamsServie.Domain.Entities;

namespace TeamsService.Infrastructure.Repositories
{
    public class LibroRepository : Repository<Libro>, ILibroRepository
    {
        public LibroRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}