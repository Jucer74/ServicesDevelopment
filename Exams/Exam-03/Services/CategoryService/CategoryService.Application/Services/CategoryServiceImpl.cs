using System.Net;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Dtos;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace CategoryService.Application.Services
{
    public class CategoryServiceImpl : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;

        public CategoryServiceImpl(ICategoryRepository CategoryRepository, IConfiguration configuration, RestClient restClient)
        {
            _categoryRepository = CategoryRepository;
            _configuration = configuration;
            _restClient = restClient;
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
            var productsResponse = await GetProductsByCategoryIdAsync(id);
            var products = JsonConvert.DeserializeObject<IEnumerable<CategoryProductDTO>>(productsResponse);
            if (products!.Count()>0)
            {
                await DeleteProductsByCategoryIdAsync(id);
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

        private async Task<String> GetProductsByCategoryIdAsync(int categoryId)
        {
            var productsServiceUrl = _configuration.GetSection("ProductsServiceUrl").Value;

            var productsEndPoint = $"{productsServiceUrl}/Category/{categoryId}";

            var request = new RestRequest(productsEndPoint);

            var response = await _restClient.GetAsync(request);

            var responseData = response.Content;

            if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException($"Error Getting products By category Id = {categoryId}");
            }

            return responseData!;
        }

        private async Task<String> DeleteProductsByCategoryIdAsync(int categoryId)
        {
            var productsServiceUrl = _configuration.GetSection("ProductsServiceUrl").Value;

            var productsEndPoint = $"{productsServiceUrl}/Category/{categoryId}";

            var request = new RestRequest(productsEndPoint);

            Console.WriteLine("HOla");

            var response = await _restClient.DeleteAsync(request);

            Console.WriteLine("HOla2");

            var responseData = response.Content;

            if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException($"Error Deleting products By category Id = {categoryId}");
            }

            return responseData!;
        }
    }
    
}
