using BankApp.Entities;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BankApp.DAL
{
    public class BankAccountRepository
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankAccountRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddAccountAsync(BankAccount account)
        {
            var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ApiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Failed to create account. Server response: {errorResponse}");
            }
        }

        public async Task<BankAccount?> GetAccountAsync(string accountNumber)
        {
            var url = $"{ApiUrl}/{accountNumber}";
            Console.WriteLine($"Fetching account from: {url}"); // Depuración

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response from server: {responseData}"); // Depuración

                return JsonSerializer.Deserialize<BankAccount>(responseData);
            }

            Console.WriteLine($"ERROR: Failed to get account. Status code: {response.StatusCode}");
            return null;
        }

        public async Task UpdateAccountAsync(BankAccount account)
        {
            var url = $"{ApiUrl}/{account.AccountNumber}";
            Console.WriteLine($"Updating account at URL: {url}");

            var content = new StringContent(JsonSerializer.Serialize(account), Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"ERROR: Failed to update account. Server response: {errorResponse}");
            }

            Console.WriteLine("Cuenta actualizada exitosamente.");
        }
    }
}
