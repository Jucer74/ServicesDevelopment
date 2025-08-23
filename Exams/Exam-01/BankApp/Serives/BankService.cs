using Entities;
using System.Text;
using System.Text.Json;

namespace Services
{
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:3000/accounts";

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            var json = JsonSerializer.Serialize(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error creating account.");
                return null;
            }

            // Extraer la respuesta para obtener el ID generado
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdAccount = JsonSerializer.Deserialize<BankAccount>(responseJson);

            return createdAccount;
        }

        public async Task<BankAccount> GetBalanceAccount(string accountNumber)
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var accounts = JsonSerializer.Deserialize<List<BankAccount>>(json);

            return accounts?.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public async Task<BankAccount> DepositAmount(string accountNumber, decimal amountValue)
        {
            var account = await GetBalanceAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return null;
            }

            account.BalanceAmount += amountValue;
            return await UpdateAccount(account);
        }

        public async Task<BankAccount> WithdrawalAmount(string accountNumber, decimal amountValue)
        {
            var account = await GetBalanceAccount(accountNumber);
            if (account == null || (account.BalanceAmount + account.OverdraftAmount) < amountValue)
            {
                Console.WriteLine("Insufficient funds or account not found.");
                return null;
            }

            account.BalanceAmount -= amountValue;
            return await UpdateAccount(account);
        }

        private async Task<BankAccount> UpdateAccount(BankAccount account)
        {
            var existingAccount = await GetBalanceAccount(account.AccountNumber);
            if (existingAccount == null || string.IsNullOrEmpty(existingAccount.Id))
            {
                Console.WriteLine("Error: Account not found or ID is missing.");
                return null;
            }

            var accountId = existingAccount.Id;  // Ahora siempre tendrá un valor correcto

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BaseUrl}/{accountId}", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error updating account.");
                return null;
            }

            return account;
        }
    }
}