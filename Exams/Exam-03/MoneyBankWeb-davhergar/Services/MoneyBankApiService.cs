using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using MoneyBankWeb_davhergar.Models;

namespace MoneyBankWeb_davhergar.Services
{
    public class MoneyBankApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = string.Empty;

        public MoneyBankApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["MoneyBankApiBaseUrl"] ?? throw new ArgumentNullException("MoneyBankApiBaseUrl");
        }

        public async Task<List<AccountDto>> GetAccountsAsync(string? accountNumber = null)
        {
            var url = string.IsNullOrEmpty(accountNumber)
                ? $"{_baseUrl}/api/Accounts"
                : $"{_baseUrl}/api/Accounts?accountNumber={accountNumber}";
            return await _httpClient.GetFromJsonAsync<List<AccountDto>>(url) ?? new List<AccountDto>();
        }

        public async Task<AccountDto?> GetAccountAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AccountDto>($"{_baseUrl}/api/Accounts/{id}");
        }

        public async Task<AccountDto?> CreateAccountAsync(AccountDto account)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/Accounts", account);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AccountDto>();
            return null;
        }

        public async Task<bool> UpdateAccountAsync(AccountDto account)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Accounts/{account.Id}", account);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Accounts/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DepositAsync(int id, TransactionDto transaction)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Accounts/{id}/Deposit", transaction);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> WithdrawalAsync(int id, TransactionDto transaction)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/Accounts/{id}/Withdrawal", transaction);
            return response.IsSuccessStatusCode;
        }
    }
}
