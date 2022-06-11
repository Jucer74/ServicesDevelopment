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

    public class ProductsService : IProductsService
    {

        private readonly IproductsRepository _ProductsRepository;
        public ProductsService(IproductsRepository ProductsRepository)
        {
            _ProductsRepository = ProductsRepository;
        }

        public async Task Add(Products entity)
        {
            await _ProductsRepository.Add(entity);

        }

        public async Task<IEnumerable<Products>> Find(Expression<Func<Products, bool>> predicate)
        {
            return await _ProductsRepository.Find(predicate);
        }

        public async Task<IEnumerable<Products>> GetAll()
        {
                return await _ProductsRepository.GetAll();
        }

        public async Task<Products> GetById(int id)
        {
            var var = await _ProductsRepository.GetById(id);
                    return var;
        }

        public async Task Remove(int id)
        {
            var var = await _ProductsRepository.GetById(id);
                await _ProductsRepository.Remove(var);
        }

        public async Task Update(Products entity)
        {
                await _ProductsRepository.Update(entity);
        }
        public async Task<IEnumerable<Products>> GetAllBycategoryId(int id)
        {
            return await _ProductsRepository.GetAllBycategoryId(id);

        }
    }
}
