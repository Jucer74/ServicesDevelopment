using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entitites;
namespace BankApp.Services
{
    public class BankService
    {
        
        private readonly string _baseUrl = "http://localhost:3000/accounts";
        private readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        public async Task<BankAccount> CreateAccountAsync(BankAccount newAccount)
        {
            newAccount.id = newAccount.AccountNumber;
            var existingAccount = await GetAccountAsync(newAccount.AccountNumber);
            if (existingAccount != null)
                throw new ArgumentException($"Account : {newAccount.AccountNumber} already exists.");

            var json = JsonSerializer.Serialize(newAccount);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
        
            var account = JsonSerializer.Deserialize<BankAccount>(responseData);
            if (account == null)
                throw new Exception("Error deserializing account.");

            return account;
        }



        public async Task<BankAccount?> GetAccountAsync(string accountNumber)
        {
            try
            {
                var url = $"{_baseUrl}?AccountNumber={accountNumber}";

                var response = await _httpClient.GetAsync(url);
            

                if (!response.IsSuccessStatusCode)
                    return null;

                var responseData = await response.Content.ReadAsStringAsync();
                
               

                var accounts = JsonSerializer.Deserialize<List<BankAccount>>(responseData);

                return accounts?.FirstOrDefault(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAccountAsync: {ex.Message}");
                return null;
            }
        }


        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber);

            if (account == null)
                throw new InvalidOperationException("Account not found.");

            Console.WriteLine($"[DEPOSIT] Initial Balance: {account.BalanceAmount}, Overdraft: {account.OverdraftAmount}");

            // El depósito solo afecta el balance
            account.BalanceAmount += amount;

            Console.WriteLine($"[DEPOSIT] New Balance: {account.BalanceAmount}, Overdraft: {account.OverdraftAmount}");

            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{account.AccountNumber}", content);

            Console.WriteLine($"[DEPOSIT] Response: {response.StatusCode}");
        }

        public async Task WithdrawalAsync(string accountNumber, decimal amountValue)
        {
            var account = await GetAccountAsync(accountNumber);

            if (account == null)
                throw new InvalidOperationException("Account not found.");

            
            decimal availableBalance = Math.Max(account.BalanceAmount - 1000000, 0);
            Console.WriteLine($"[WITHDRAWAL] Initial Balance: {account.BalanceAmount}, Overdraft: {account.OverdraftAmount}, Available Balance: {availableBalance}");
            if (account.BalanceAmount < amountValue)
                throw new InvalidOperationException("Insufficient funds.");

            if (availableBalance >= amountValue)
            {
                
                account.BalanceAmount -= amountValue;
            }
            else
            {   
               
                decimal remaining = amountValue - availableBalance;
                account.BalanceAmount =account.BalanceAmount - availableBalance - remaining;
                account.OverdraftAmount += remaining;
                }
           

           
            Console.WriteLine($"[WITHDRAWAL] New Balance: {account.BalanceAmount}, Overdraft: {account.OverdraftAmount}");
            var json = JsonSerializer.Serialize(account);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{account.AccountNumber}", content);
         }
    
    }








}
    