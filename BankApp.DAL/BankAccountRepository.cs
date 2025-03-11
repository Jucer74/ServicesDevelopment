using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.DAL
{
    public class BankAccountRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankAccountRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task AddAccount(BankAccount account)
        {
            var content = new StringContent(JsonSerializer.Serialize(account), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<BankAccount?> GetAccount(string accountNumber)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}?accountNumber={accountNumber}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(responseBody);

            return accounts?.FirstOrDefault();
        }

        public async Task<List<BankAccount>> GetAccounts()
        {
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BankAccount>>(responseBody) ?? new List<BankAccount>();
        }
    }
}