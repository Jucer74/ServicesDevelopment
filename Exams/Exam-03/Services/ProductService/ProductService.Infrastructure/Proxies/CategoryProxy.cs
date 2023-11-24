using Microsoft.Extensions.Options;
using ProductService.Domain.Entities;
using ProductService.Domain.Exceptions;
using ProductService.Domain.Interfaces.Proxies;
using ProductService.Infrastructure.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.Proxies
{
    public class CategoryProxy : ICategoryProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;


        public CategoryProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient)
        {
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category gettedCategory = new();
            var response = await _httpClient.GetAsync(_apiUrls.Categories + "/" + id);
            if(!response.IsSuccessStatusCode)
            {
                throw new NotFoundException(response.StatusCode.ToString());
            }
            gettedCategory = await response.Content.ReadFromJsonAsync<Category>() ?? gettedCategory;
            return gettedCategory;
        }
    }
}
