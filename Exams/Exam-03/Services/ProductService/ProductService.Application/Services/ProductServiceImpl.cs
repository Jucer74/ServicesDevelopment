using ProductServiceAPI.Application.Interfaces;
using ProductServiceAPI.Domain.Entities;
using ProductServiceAPI.Domain.Exceptions;
using ProductServiceAPI.Domain.Interfaces.Repositories;
using ProductServiceAPI.Utilities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using ProductService.Domain.Dtos;

namespace ProductServiceAPI.Application.Services
{
   public class ProductServiceImpl : IProductService
   {
      private readonly IProductRepository _productRepository;
        private readonly IConfiguration _configuration;
        private readonly RestClient _restClient;

        public ProductServiceImpl(IProductRepository ProductRepository, IConfiguration configuration, RestClient restClient)
      {
         _productRepository = ProductRepository;
            _configuration = configuration;
            _restClient = restClient;
      }

      public async Task<Product> AddAsync(Product product)
      {
         if (!await CategoryExists(product.CategoryId))
         {
            throw new NotFoundException($"Category [{product.CategoryId}] Not Found");
         }
         if (!Ean13Calculator.IsValid(product.EanCode))
         {
             throw new BadRequestException($"EAN Code [{product.EanCode}] is Not Valid");
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

        public async Task RemoveByCategoryIdAsync(int categoryId)
        {
            var product = await GetProductsByCategoryId(categoryId);
            if (product is null)
            {
                throw new NotFoundException($"Products with category [{categoryId}] Not Found");
            }

            await _productRepository.RemoveRangeAsync(product);
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
         var categoryServiceUrl = _configuration.GetSection("CategoriesServiceUrl").Value;

         var productsEndPoint = $"{categoryServiceUrl}/{categoryId}";

         var request = new RestRequest(productsEndPoint);

         var response = await _restClient.GetAsync(request);

         var responseData = response.Content;

         if (!response.IsSuccessful && response.StatusCode != HttpStatusCode.OK)
         {
            return false;
         }
         else {
            return true!;
         }
      }
   }
}