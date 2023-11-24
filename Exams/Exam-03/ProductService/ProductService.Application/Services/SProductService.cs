using Microsoft.Extensions.Configuration;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Exceptions;
using ProductService.Domain.Interfaces.Repositories;
using ProductService.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Services
{
    public class SProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly RestClient _restClient;

        public SProductService(IProductRepository ProductRepository, IConfiguration configuration)
        {
            string baseUrl = configuration["CategoryServiceUrl"]!;
            _restClient = new RestClient(baseUrl);
            _productRepository = ProductRepository;
        }

        public async Task<Product> AddAsync(Product product)
        {
            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            if (!await CategoryExists(product.CategoryId))
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }

            return await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException($"Product [{id}] Not Found");
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            if (!await CategoryExists(categoryId))
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }

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

        public async Task UpdateAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Product.Id [{product.Id}]");
            }

            if (!Ean13Calculator.IsValid(product.EanCode))
            {
                throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
            }

            if (!await CategoryExists(product.CategoryId))
            {
                throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
            }

            await _productRepository.UpdateAsync(product);
        }

        private async Task<bool> CategoryExists(int categoryId)
        {   

            var request = new RestRequest($"api/v1.0/Categories/{categoryId}", Method.Get);
            var response = await _restClient.ExecuteAsync<Category>(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new NotFoundException($"Category [{categoryId}] Not Found");
            }
            if (!response.IsSuccessful)
            {
                throw new InternalServerErrorException("Something went wrong");
            }
            return true;
        }

        public async Task RemoveProductsByCategoryId(int categoryId)
        {

            var products = await _productRepository.FindAsync(p => p.CategoryId == categoryId);

            if (products.Any())
            {
                await _productRepository.RemoveRangeAsync(products);
            }

        }
    }
}
