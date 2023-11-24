using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Application.Services
{
    public class SCategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        private readonly RestClient _restClient;

        public SCategoryService(ICategoryRepository CategoryRepository, IConfiguration configuration)
        {
            string baseUrl = configuration["ProductServiceUrl"]!;
            _restClient = new RestClient(baseUrl);
            _categoryRepository = CategoryRepository;
        }

        public async Task<Category> AddAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            return category;
        }

        public async Task RemoveAsync(int id)
        {
            var products = await GetProductsByCategoryIdAsync(id);
            if (products.Any())
            {
                var request = new RestRequest($"api/v1.0/Products/Category/{id}", Method.Delete);
                var response = await _restClient.ExecuteAsync(request);

                if (!response.IsSuccessful)
                {
                    throw new InternalServerErrorException("Something went wrong");
                }
                
            }

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            await _categoryRepository.RemoveAsync(category);
        }

        public async Task UpdateAsync(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
            }

            await _categoryRepository.UpdateAsync(category);
        }
        private async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var request = new RestRequest($"api/v1.0/Products/Category/{categoryId}", Method.Get);
            var response = await _restClient.ExecuteAsync<List<Product>>(request);
            if (response.StatusCode== System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }
            if (!response.IsSuccessful)
            {
                throw new InternalServerErrorException($"{response.ErrorMessage} {response.StatusCode}");
            }
            return response.Data!;
        }
    }
}
