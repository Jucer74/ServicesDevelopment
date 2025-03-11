using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "http://localhost:3000/accounts"; // URL del json-server

        // Obtener todas las cuentas desde el json-server
        public async Task<List<IBankAccount>> GetAccountsAsync()
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IBankAccount>>(json);
        }

        // Agregar una nueva cuenta al json-server
        public async Task AddAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        // Actualizar una cuenta en el json-server
        public async Task UpdateAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiUrl}/{account.AccountNumber}", content);
            response.EnsureSuccessStatusCode();
        }
    }

    
}

