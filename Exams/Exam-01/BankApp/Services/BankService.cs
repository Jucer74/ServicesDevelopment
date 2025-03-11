using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using BankApp.Entities;

namespace BankApp.Services
{
    // BankService se encarga de gestionar las operaciones sobre las cuentas bancarias utilizando json‑server.
    public class BankService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "http://localhost:3000/accounts"; // Asegúrate de que este endpoint coincida con tu json-server

        public BankService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> CreateAccountAsync(IBankAccount account)
        {
            var existingAccount = await GetAccountAsync(account.AccountNumber);
            if (existingAccount != null)
            {
                Console.WriteLine($"Account {account.AccountNumber} already exists.");
                return false;
            }

            var accountToPost = new
            {
                id = account.AccountNumber,
                account.AccountNumber,
                account.AccountOwner,
                BalanceAmount = account.BalanceAmount,
                AccountType = account.AccountType.ToString(),
                OverdraftAmount = (account is CheckingAccount checking) ? checking.OverdraftAmount : (decimal?)null
            };

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(baseUrl, accountToPost);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Account Created.");
                return true;
            }
            else
            {
                Console.WriteLine("Error creating account.");
                return false;
            }
        }

        // Método actualizado para obtener una cuenta usando JsonElement
        public async Task<IBankAccount> GetAccountAsync(string accountNumber)
        {
            try
            {
                // Se obtiene la lista de cuentas desde json‑server
                var accounts = await _httpClient.GetFromJsonAsync<List<Dictionary<string, object>>>(baseUrl);
                if (accounts != null)
                {
                    foreach (var dict in accounts)
                    {
                        // Comprobamos que la clave "id" (que se usó como AccountNumber) coincida
                        if (dict.ContainsKey("id") && dict["id"].ToString() == accountNumber)
                        {
                            // Leer "accountType" usando JsonElement
                            string accountTypeStr = "Saving";
                            if (dict.ContainsKey("accountType"))
                            {
                                var typeElement = (JsonElement)dict["accountType"];
                                accountTypeStr = typeElement.GetString();
                            }
                            AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), accountTypeStr);

                            // Leer "accountOwner"
                            string owner = "";
                            if (dict.ContainsKey("accountOwner"))
                            {
                                var ownerElement = (JsonElement)dict["accountOwner"];
                                owner = ownerElement.GetString();
                            }

                            // Leer "balanceAmount"
                            decimal balance = 0;
                            if (dict.ContainsKey("balanceAmount"))
                            {
                                var balanceElement = (JsonElement)dict["balanceAmount"];
                                balance = balanceElement.GetDecimal();
                            }

                            if (accountType == AccountType.Checking)
                            {
                                decimal overdraft = 0;
                                if (dict.ContainsKey("overdraftAmount"))
                                {
                                    var overdraftElement = (JsonElement)dict["overdraftAmount"];
                                    if (overdraftElement.ValueKind != JsonValueKind.Null)
                                        overdraft = overdraftElement.GetDecimal();
                                }
                                // Para una cuenta de Checking, se resta el sobregiro mínimo (1,000,000) del balance almacenado.
                                return new CheckingAccount(accountNumber, owner, balance - 1000000) { OverdraftAmount = overdraft };
                            }
                            else
                            {
                                return new SavingAccount(accountNumber, owner, balance);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting account: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> DepositAccountAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine($"Account {accountNumber} doesn't exist.");
                return false;
            }
            account.Deposit(amount);

            var accountToPut = new
            {
                id = account.AccountNumber,
                account.AccountNumber,
                account.AccountOwner,
                BalanceAmount = account.BalanceAmount,
                AccountType = account.AccountType.ToString(),
                OverdraftAmount = (account is CheckingAccount checking) ? checking.OverdraftAmount : (decimal?)null
            };

            var putResponse = await _httpClient.PutAsJsonAsync($"{baseUrl}/{account.AccountNumber}", accountToPut);
            if (putResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Deposit Success.");
                return true;
            }
            else
            {
                Console.WriteLine("Error updating account after deposit.");
                return false;
            }
        }

        public async Task<bool> WithdrawalAccountAsync(string accountNumber, decimal amount)
        {
            var account = await GetAccountAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine($"Account {accountNumber} doesn't exist.");
                return false;
            }
            try
            {
                account.Withdrawal(amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            var accountToPut = new
            {
                id = account.AccountNumber,
                account.AccountNumber,
                account.AccountOwner,
                BalanceAmount = account.BalanceAmount,
                AccountType = account.AccountType.ToString(),
                OverdraftAmount = (account is CheckingAccount checking) ? checking.OverdraftAmount : (decimal?)null
            };

            var putResponse = await _httpClient.PutAsJsonAsync($"{baseUrl}/{account.AccountNumber}", accountToPut);
            if (putResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Withdrawal Success.");
                return true;
            }
            else
            {
                Console.WriteLine("Error updating account after withdrawal.");
                return false;
            }
        }

        public async Task GetBalanceAccountAsync(string accountNumber)
        {
            var account = await GetAccountAsync(accountNumber);
            if (account == null)
            {
                Console.WriteLine($"Account {accountNumber} doesn't exist.");
                return;
            }
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Account Owner: {account.AccountOwner}");
            Console.WriteLine($"Account Type: {account.AccountType}");
            Console.WriteLine($"Balance Amount: {account.BalanceAmount}");
            if (account is CheckingAccount checking)
            {
                Console.WriteLine($"Overdraft Amount: {checking.OverdraftAmount}");
            }
        }
    }
}
