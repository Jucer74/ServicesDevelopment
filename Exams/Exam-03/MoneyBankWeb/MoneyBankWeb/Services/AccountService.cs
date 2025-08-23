using MoneyBankWeb_josoterad.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MoneyBankWeb_josoterad.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://localhost:5001/api/accounts"; // Cambia al URL real de tu API

        public AccountService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<AccountDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<AccountDto>>(_baseUrl);
        }

        public async Task<AccountDto> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AccountDto>($"{_baseUrl}/{id}");
        }

        public async Task<bool> CreateAsync(AccountDto account)
        {
            var response = await _httpClient.PostAsJsonAsync(_baseUrl, account);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(AccountDto account)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{account.Id}", account);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
