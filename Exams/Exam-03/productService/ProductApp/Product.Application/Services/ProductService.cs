using Microsoft.Extensions.Configuration;
using Product.Application.Interfaces;
using Product.Domain.Entities;
using Product.Domain.Exceptions;
using Product.Domain.Interfaces.Repositories;
using Product.Utilities;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly RestClient _restClient;

        public ProductService(IProductRepository ProductRepository, IConfiguration configuration)
        {
            _productRepository = ProductRepository;
            _restClient = new RestClient(configuration["MicroserviceSettings:CategoriesServiceUrl"]!);
        }

        public async Task<EProduct> AddAsync(EProduct product)
        {
            Category category = await FetchCategoryById(product.CategoryId);

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            if (category.Description.ToLower() != product.CategoryName.ToLower())
            {
                throw new BadRequestException($"Category's name {category.Description} does not match with Product's category name {product.CategoryName}");
            }

            return await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<EProduct>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<EProduct> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            return product;
        }

        public async Task<IEnumerable<EProduct>> GetProductsByCategoryId(int categoryId)
        {
            return await _productRepository.FindAsync(p => p.CategoryId == categoryId);
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            await _productRepository.RemoveAsync(product);
        }

        public async Task UpdateAsync(int id, EProduct product)
        {
            Category category = await FetchCategoryById(product.CategoryId);
            var productToUpdate = await _productRepository.GetByIdAsync(id);

            if (productToUpdate is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            if (id != product.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
            }

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            if (category.Description.ToLower() != product.CategoryName.ToLower())
            {
                throw new BadRequestException($"Category's name {category.Description} does not match with Product's category name {product.CategoryName}");
            }

            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveProductsByCategoryId(int categoryId)
        {

            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);

            if (products.Any())
            {
                await _productRepository.RemoveRangeAsync(products);
            }
            
        }

        private async Task<Category> FetchCategoryById(int categoryId)
        {
            var request = new RestRequest($"/{categoryId}", Method.Get);
            var response = await _restClient.ExecuteAsync<Category>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
               throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

            if (!response.IsSuccessful)
            {
                throw new InternalServerErrorException($"Error fetching Category [{categoryId}]");
            }

            return response.Data!;
        }
    }
}