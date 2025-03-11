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
        
        private readonly string _baseUrl = Config.GetApiUrl() ;
        private readonly HttpClient _httpClient;

        public BankService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    
        public async Task<BankAccount> CreateAccountAsync(BankAccount newAccount)
        {
            try
            {
                
                newAccount.id = newAccount.AccountNumber;

            
                var existingAccount = await GetAccountAsync(newAccount.AccountNumber);
                if (existingAccount != null)
                    throw new ArgumentException($"Account: {newAccount.AccountNumber} already exists.");

            
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
            catch (Exception ex)
            {
            
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
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
        {   try
                {
                var account = await GetAccountAsync(accountNumber);

                if (account == null)
                    throw new InvalidOperationException("Account not found.");

                
            

                
                if (account.OverdraftAmount > 0)
                {
                    

                    if (amount <= account.OverdraftAmount)
                    {
                        account.OverdraftAmount -= amount; 
                    
                        amount = 0;
                    }
                    else
                    {
                    
                        amount -= account.OverdraftAmount;
                        account.OverdraftAmount = 0;
                    
                    }
                }

            
                account.BalanceAmount += amount;

            

                var json = JsonSerializer.Serialize(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{account.AccountNumber}", content);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
         
        }


        public async Task WithdrawalAsync(string accountNumber, decimal amountValue)
        {
             try
            {       
                var account = await GetAccountAsync(accountNumber);
                
                if (account == null)
                    throw new InvalidOperationException("Account not found.");

                    
                decimal totalBalance = account.BalanceAmount;
                if (totalBalance < amountValue)
                    throw new InvalidOperationException("Insufficient funds.");
                if(account.AccountType == 2)
                    {
                decimal availableBalance = totalBalance-1000000; 
                if (availableBalance < 0)
                    availableBalance = 0;
            
                if (availableBalance >= amountValue)
                {
                    
                    account.BalanceAmount -= amountValue;
                }
                else
                {
                    
                    decimal remaining = amountValue - availableBalance;
                    account.BalanceAmount =totalBalance - amountValue ; 
                    account.OverdraftAmount += remaining; 
                }
                }
                else
                {

                    account.BalanceAmount -= amountValue;
                }
                

                var json = JsonSerializer.Serialize(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{account.AccountNumber}", content);
            }   
             catch (Exception ex)
            {
               
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }
    
    }

}
    