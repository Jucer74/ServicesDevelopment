using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;
using Microsoft.Extensions.Configuration;



namespace BankApp.Services
{
    public class DbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public DbService()
        {
            _httpClient = new HttpClient();
            
            
            var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "UI")) // Indica la carpeta UI
                .AddJsonFile("appsettings.json")
                .Build();
            
            _baseUrl = config["JsonServer:BaseUrl"];
        }

        public async Task<List<IBankAccount>> GetAccountsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/accounts");
            return JsonSerializer.Deserialize<List<IBankAccount>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> CreateAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/accounts", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<IBankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/accounts?accountNumber={accountNumber}");
            var accounts = JsonSerializer.Deserialize<List<IBankAccount>>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return accounts?.Count > 0 ? accounts[0] : null;
        }

        public async Task<bool> UpdateAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/accounts/{account.AccountNumber}", content);
            return response.IsSuccessStatusCode;
        }
    }
}
