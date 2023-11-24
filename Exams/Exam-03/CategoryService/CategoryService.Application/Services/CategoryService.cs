using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace CategoryService.Application.Services
{
    public class UCCategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly RestClient _restClient;

        public UCCategoryService(ICategoryRepository CategoryRepository, IConfiguration configuration)
        {
            string baseUrl = configuration["MicroserviceSettings:ProductServiceUrl"]!;
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
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null)
            {
                throw new NotFoundException($"Category [{id}] Not Found");
            }

            var proudcts = await GetProductsByCategoryId(id);

            if (proudcts.Any())
            {
                await DeleteProductsByCategoryId(id);
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

        private async Task<List<Product>> GetProductsByCategoryId(int id)
        {
            RestRequest restRequest = new($"/Category/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync<List<Product>>(restRequest);

            if (!(response.IsSuccessful))
                throw new InternalServerErrorException($"Error getting products from category [{id}]");

            return response.Data!;
        } 


        private async Task DeleteProductsByCategoryId(int id)
        {
            RestRequest restRequest = new($"/Category/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(restRequest);

            if (!(response.IsSuccessful))
                throw new InternalServerErrorException($"Error deleting products from category [{id}]");
        }
    }
}