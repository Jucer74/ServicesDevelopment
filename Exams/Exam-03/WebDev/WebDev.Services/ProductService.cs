using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebDev.Services.Entities;

namespace WebDev.Services
{
    public class ProductService
    {
        private string BaseUrl { get; }
        private HttpClient _httpClient;

        public ProductService(string baseUrl)
        {
            BaseUrl = baseUrl;
            _httpClient = new HttpClient();
            SetupHttpConnection(_httpClient, baseUrl);
        }

        private void SetupHttpConnection(HttpClient httpClient, string baseUrl)
        {
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ProductDto>> GetProducts()
        {
            var librosList = new List<ProductDto>();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Products");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                librosList = JsonConvert.DeserializeObject<List<ProductDto>>(responseContent);
            }

            return librosList;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            ProductDto libro = null;

            HttpResponseMessage response = await _httpClient.GetAsync($"api/Products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                libro = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            }

            return libro;
        }

        public async Task<ProductDto> AddProduct(ProductDto libro)
        {
            ProductDto libroDtoResponse = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"api/Products", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                libroDtoResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            }

            return libroDtoResponse;
        }

        public async Task<ProductDto> UpdateProduct(ProductDto libro)
        {
            ProductDto libroDtoResponse = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(libro), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"api/Products/{libro.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                libroDtoResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            }

            return libroDtoResponse;
        }

        public async Task<ProductDto> DeleteProduct(int id)
        {
            ProductDto libroDtoResponse = null;

            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                libroDtoResponse = JsonConvert.DeserializeObject<ProductDto>(responseContent);
            }

            return libroDtoResponse;
        }
    }
}
