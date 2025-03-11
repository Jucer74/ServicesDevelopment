using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using Models;
using Newtonsoft.Json;

namespace BankServices {
    public class BankService 
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:3000/api/v1";

        public BankService()
        {
            _httpClient = new HttpClient(); 
        }

        public async Task<List<BankAccount>> GetAccount(string accountNumber) 
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/accounts?AccountNumber={accountNumber}");
            
            if (!response.IsSuccessStatusCode) return new List<BankAccount>();
            var json = await response.Content.ReadAsStringAsync();
            return  JsonConvert.DeserializeObject<List<BankAccount>>(json) ?? new List<BankAccount>();
        }

      
        public async Task<BankAccount?> CreateAccount(BankAccount bankAccount)
        {
            var existingAccounts = await GetAccount(bankAccount.AccountNumber);
            
            if (existingAccounts != null && existingAccounts.Count > 0)
            {
                Console.WriteLine("Account already exists!");
                return null;
            }   

            Console.WriteLine(bankAccount);
            string json = JsonConvert.SerializeObject(bankAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{ApiUrl}/accounts", content);
            response.EnsureSuccessStatusCode();

            return bankAccount;
        }

        public async Task<decimal?> GetBalance(string accountNumber)
        {
            var accounts = await GetAccount(accountNumber);
            var account = accounts.FirstOrDefault(); // Get the first account or null
            if (account == null)
            {
                    Console.WriteLine("Account Not Found");
                    return null;
            }

           return account.BalanceAmount;
        }
        
    }

}

