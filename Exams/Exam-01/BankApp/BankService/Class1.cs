using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Models;

namespace BankService {
    public class BankService 
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/api/v1";

        public BankService()
        {
            _httpClient = new HttpClient(); 
        }

        public async Task<BankAccount> GetAccount(string accountNumber) 
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/accounts/{accountNumber}");
            
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsByteArrayAsync();
            return JsonSerializer.Deserialize<BankAccount>(json); //take care
        }

        public async Task<bool> ExistsAccount(string accountNumber)
        {
            return await GetAccount(accountNumber) != null;
        } 

        public async Task<IBankAccount> CreateAccount(IBankAccount bankAccount)
        {
            if (await ExistsAccount(bankAccount.AccountNumber))
            {
                Console.WriteLine("Account already exists!");
                return null;
            }   

            string json = JsonSerializer.Serialize(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ApiUrl}/accounts", content);
            response.EnsureSuccessStatusCode();

            return bankAccount;
        }

        public async Task<decimal?> GetBalance(string accountNumber)
        {
           var account = await GetAccount(accountNumber);
           if (account == null)
           {
                Console.WriteLine("Account Not Found");
                return null;
           }

           return account.BalanceAmount;
        }
       
    }

}

