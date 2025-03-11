using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.DAL
{
    public class BankRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<IBankAccount>> GetAllAccountsAsync()
        {
            var response = await _httpClient.GetStringAsync(ApiUrl);
            var accounts = JsonSerializer.Deserialize<List<CheckingAccount>>(response);
            return accounts.Cast<IBankAccount>().ToList();
        }

        public async Task<IBankAccount> GetAccountByNumberAsync(string accountNumber)
        {
            var accounts = await GetAllAccountsAsync();
            return accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
        }

        public async Task CreateAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(ApiUrl, content);
        }

        public async Task UpdateAccountAsync(IBankAccount account)
        {
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{ApiUrl}/{account.AccountNumber}", content);
        }
    }
}
