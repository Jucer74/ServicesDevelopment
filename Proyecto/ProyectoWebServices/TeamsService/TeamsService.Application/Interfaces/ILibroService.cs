using TeamsService.Domain.Dtos;
using TeamsServie.Domain.Entities;

namespace TeamsService.Application.Interfaces;

public interface ILibroService
{
    Task<Libro> CreateTeam(Libro libro);

    Task DeleteTeam(int id);

    Task<IEnumerable<Libro>> GetAllTeams();

    Task<Libro> GetTeamById(int id);

    Task<Libro> UpdateTeam(int id, Libro libro);

    Task<IEnumerable<AutorDTO>> GetAutoresByTeamId(int id);
}