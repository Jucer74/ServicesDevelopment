using System.Linq.Expressions;
using Pricat.Application.Interfaces;
using Pricat.Application.Interfaces.Repositories;
using Pricat.Domain.Entities;

namespace Pricat.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Categories> AddAsync(Categories entity)
        {
            return await _categoryRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<Categories>> FindAsync(Expression<Func<Categories, bool>> predicate)
        {
            return await _categoryRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Categories> GetByIdAsync(int id)
        {
            var categori = await _categoryRepository.GetByIdAsync(id);

            if (categori == null)
            {
                throw new Exception($"Categori with Id={id} Not Found");
            }
            return categori;
        }

        public async Task<Categories> UpdateAsync(int id, Categories entity)
        {
            if (id != entity.Id)
            {
                throw new Exception($"Categori with Id={id} mismatch.");
            }

            var categori = await _categoryRepository.GetByIdAsync(id);
            if (categori == null)
            {
                throw new Exception($"Categori with Id={id} Not Found");
            }

            return await _categoryRepository.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(int id)
        {
            var categori = await _categoryRepository.GetByIdAsync(id);
            if (categori == null)
            {
                throw new Exception($"Categori with Id={id} Not Found");
            }

            await _categoryRepository.DeleteAsync(categori);
        }
    }
}
