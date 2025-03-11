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

        public async Task<BankAccount?> GetAccount(string accountNumber) 
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/accounts/{accountNumber}");
            
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return  JsonConvert.DeserializeObject<BankAccount>(json)!;
        }
        public async Task<BankAccount?> CreateAccount(BankAccount bankAccount)
        {
            var existingAccount = await GetAccount(bankAccount.AccountNumber);
            if (existingAccount != null)
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
            var account = await GetAccount(accountNumber);
            if (account == null)
            {
                    Console.WriteLine("Account Not Found");
                    return null;
            }

           return account.BalanceAmount;
        }
        
        public async Task<BankAccount?> DepositAmount(string accountNumber, decimal amountValue)
            {
            var account = await GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return null;
            }

            account.BalanceAmount += amountValue;
            string json = JsonConvert.SerializeObject(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{ApiUrl}/accounts/{accountNumber}", content);
            response.EnsureSuccessStatusCode();

            return account;
        }

        public async Task<BankAccount?> WithdrawalAccount(string accountNumber, decimal amount) 
       {
            var account = await GetAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found.");
                return null;
            }

            if (account.AccountType == 1 || account.BalanceAmount >= amount )
            {
                account.BalanceAmount -= amount;
                string json = JsonConvert.SerializeObject(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{ApiUrl}/accounts/{accountNumber}", content);
                response.EnsureSuccessStatusCode();
            }
            else 
            {
                Console.WriteLine("Insufficient funds.");
                return null;
            }
            
            return account;
       }
    }

}

