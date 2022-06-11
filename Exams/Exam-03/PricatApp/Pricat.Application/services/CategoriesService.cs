using Application.Exceptions;
using Pricat.Application.interfaces;
using Pricat.Domain.Entities;
using Pricat.Domain.interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pricat.Application.services
{

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoryRepository;
        public CategoriesService(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Add(Categories entity)
        {
            await _categoryRepository.Add(entity);
        }

        public async Task<IEnumerable<Categories>> Find(Expression<Func<Categories, bool>> predicate)
        {
            return await _categoryRepository.Find(predicate);
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
    
                return await _categoryRepository.GetAll();
     
            
        }

        public async Task<Categories> GetById(int id)
        {
            var var = await _categoryRepository.GetById(id);
                    return var;
        }

        public async Task Remove(int id)
        {
            var var = await _categoryRepository.GetById(id);
                await _categoryRepository.Remove(var);

        }

        public async Task Update(Categories entity)
        {

                await _categoryRepository.Update(entity);


        }

    }
}
