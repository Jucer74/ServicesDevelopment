using Application.Exceptions;
using PricatApp.Application.Interfaces;
using PricatApp.Domain.Entities;
using PricatApp.Domain.Exceptions;
using PricatApp.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PricatApp.Application.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly ICategorieRepository _categorieRepository;

        public CategorieService(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        public async Task<Categorie> AddAsync(Categorie entity)
        {
            return await _categorieRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Categorie>> FindAsync(Expression<Func<Categorie, bool>> predicate)
        {
            return await _categorieRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Categorie>> GetAllAsync()
        {
            return await _categorieRepository.GetAllAsync();
        }

        public async Task<Categorie> GetByIdAsync(int id)
        {
            var categorie = await _categorieRepository.GetByIdAsync(id);

            // Validte If Exist
            return categorie;
        }

        public async Task RemoveAsync(int id)
        {
            var categorie = await _categorieRepository.GetByIdAsync(id);

            if (categorie is null)
            {
                throw new NotFoundException($"Categorie with Id={id} Not Found");
            }

            await _categorieRepository.RemoveAsync(categorie);
        }

        public async Task<Categorie> UpdateAsync(int id, Categorie entity)
        {
            if (id != entity.Id)
            {

                throw new BadRequestException($"The Id={id} not corresponding with Entity.Id={entity.Id}");
            }
            var categorie = await _categorieRepository.GetByIdAsync(id);

            if (categorie is null)
            {
                throw new NotFoundException($"Categorie with Id={id} Not Found");
            }
            return (await _categorieRepository.UpdateAsync(entity));
        }
    }
}