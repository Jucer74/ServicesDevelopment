using CategoryService.Domain.Entities;
using CategoryService.Domain.Exceptions;
using CategoryService.Domain.Interfaces.Proxies;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CategoryService.Infrastructure.Proxies
{
    public class ProductProxy : IProductProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;


        public ProductProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
        {
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            IEnumerable<Product> productsByCategory = new List<Product>();
            var response = await _httpClient.GetAsync(_apiUrls.Products + "/Category/" + categoryId);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException(response.StatusCode.ToString());
            }
            productsByCategory = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>() ?? productsByCategory;
            return productsByCategory;
        }

        public async Task RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrls.Products + "/" + id);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> products)
        {
            foreach(Product product in products)
            {
                await RemoveAsync(product.Id);
            }
        }
    }
}
