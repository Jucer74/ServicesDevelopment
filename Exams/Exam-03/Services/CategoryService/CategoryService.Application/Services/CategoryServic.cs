using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CategoryService.Application.Interfaces;
using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Repositories;
using CategoryService.Exceptions;
using CategoryService.Domain.Dtos;

namespace Categoryservice.Application.Services
{
    public class CategoryServic : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;

        public CategoryServic(ICategoryRepository categoryRepository, IConfiguration configuration, RestClient restClient)
        {
            _categoryRepository = categoryRepository;
            _configuration = configuration;
            _restClient = restClient;
        }

        public async Task<Category> AddCategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task RemoveCategory(int id)
        {
            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is not null)
            {
                await _categoryRepository.RemoveAsync(original);
                return;
            }

            throw new NotFoundException($"Category with Id={id} Not Found");
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category is not null)
            {
                return category;
            }

            throw new NotFoundException($"Category with Id={id} Not Found");
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Category.Id [{category.Id}]");
            }

            var original = await _categoryRepository.GetByIdAsync(id);

            if (original is not null)
            {
                return await _categoryRepository.UpdateAsync(category);
            }

            throw new NotFoundException($"Category with Id={id} Not Found");
        }

        public async Task<IEnumerable<CategoryProductDto>> GetProductByCategoryId(int id)
        {
            // Call the Products Service
            var productsResponse = await CallProductsService(id);
            var products = JsonConvert.DeserializeObject<IEnumerable<CategoryProductDto>>(productsResponse);
            return products!;
        }

        private async Task<string> CallProductsService(int Id)
        {
            var productServiceUrl = _configuration.GetSection("ProductServiceUrl").Value;

            var productsEndPoint = $"{productServiceUrl}/Category/{Id}";

            var request = new RestRequest(productsEndPoint);

            var response = await _restClient.GetAsync(request);

            var responseData = response.Content;

            if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.OK)
            {
                throw new InternalServerErrorException($"Error Getting Products By Category Id = {Id}");
            }

            return responseData!;
        }

        

        
    }
}