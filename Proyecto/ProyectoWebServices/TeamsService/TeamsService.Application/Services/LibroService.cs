using TeamsService.Application.Interfaces;
using TeamsService.Domain.Dtos;

using TeamsService.Domain.Exceptions;
using TeamsService.Domain.Interfaces.Repositories;
using TeamsServie.Domain.Entities;

namespace TeamsService.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IAutorRepository _memberRepository;

        public LibroService(ILibroRepository teamRepository, IAutorRepository memberRepository)
        {
            _libroRepository = teamRepository;
            _memberRepository = memberRepository;
        }

        public async Task<Libro> CreateTeam(Libro libro)
        {
            return await _libroRepository.AddAsync(libro);
        }

        public async Task DeleteTeam(int id)
        {
            var original = await _libroRepository.GetByIdAsync(id);

            if (original is not null)
            {
                await _libroRepository.RemoveAsync(original);
                await _memberRepository.RemoveMembersByTeamId(id);
                return;
            }

            throw new NotFoundException($"Libro with Id={id} Not Found");
        }

        public async Task<IEnumerable<Libro>> GetAllTeams()
        {
            return await _libroRepository.GetAllAsync();
        }

        public async Task<Libro> GetTeamById(int id)
        {
            var libro = await _libroRepository.GetByIdAsync(id);

            if (libro is not null)
            {
                return libro;
            }

            throw new NotFoundException($"Libro with Id={id} Not Found");
        }

        public async Task<Libro> UpdateTeam(int id, Libro libro)
        {
            if (id != libro.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Libro.Id [{libro.Id}]");
            }

            var original = await _libroRepository.GetByIdAsync(id);

            if (original is not null)
            {
                return await _libroRepository.UpdateAsync(libro);
            }

            throw new NotFoundException($"Libro with Id={id} Not Found");
        }

        public async Task<IEnumerable<AutorDTO>> GetAutoresByTeamId(int id)
        {
            var libro = await _libroRepository.GetByIdAsync(id);

            if (libro is null)
            {
                throw new NotFoundException($"Libro with Id={id} Not Found");
            }
                        
            var members = await _memberRepository.GetMembersByTeamId(id);

            return members;
        }
    }
}