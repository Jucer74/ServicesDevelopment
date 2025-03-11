using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using BankApp.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BankApp.BL
{
    public class BankAccountRepository
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "http://localhost:3000/accounts"; // Asegúrate de que json-server está corriendo

        public BankAccountRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<CheckingAccount>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<CheckingAccount>>(BaseUrl) ?? new List<CheckingAccount>();
        }

        public async Task<CheckingAccount?> GetByAccountNumber(string accountNumber)
        {
            var accounts = await GetAll();
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public async Task<bool> Exists(string accountNumber)
        {
            var account = await GetByAccountNumber(accountNumber);
            return account != null;
        }

        public async Task Create(CheckingAccount account)
        {
            var exists = await Exists(account.AccountNumber);
            if (exists)
                throw new InvalidOperationException($"Account {account.AccountNumber} already exists.");

            var response = await _client.PostAsJsonAsync(BaseUrl, account);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error creating account in json-server.");
        }

        public async Task Update(CheckingAccount account)
        {
            var url = $"{BaseUrl}/{account.AccountNumber}";
            var response = await _client.PutAsJsonAsync(url, account);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error updating account in json-server.");
        }
    }
}