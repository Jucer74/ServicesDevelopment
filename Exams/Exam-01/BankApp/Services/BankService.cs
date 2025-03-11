using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        // Crea una nueva cuenta en json‑server después de verificar que no exista.
        public async Task<bool> CreateAccountAsync(IBankAccount account)
        {
            // Verificar si la cuenta ya existe
            var existingAccount = await GetAccountAsync(account.AccountNumber);
            if (existingAccount != null)
            {
                Console.WriteLine($"Account {account.AccountNumber} already exists.");
                return false;
            }

            // Para que json‑server use nuestro número de cuenta como identificador (id),
            // creamos un objeto anónimo que incluya la propiedad "id" igual a AccountNumber.
            var accountToPost = new
            {
                id = account.AccountNumber,
                account.AccountNumber,
                account.AccountOwner,
                account.BalanceAmount,
                AccountType = account.AccountType.ToString(), // se almacena como cadena
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

        // Obtiene una cuenta por AccountNumber (usado también como id en json‑server)
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
                        if (dict.ContainsKey("id") && dict["id"].ToString() == accountNumber)
                        {
                            // Reconstruimos el objeto a partir de los datos obtenidos.
                            string accountTypeStr = dict.ContainsKey("AccountType") ? dict["AccountType"].ToString() : "Saving";
                            AccountType accountType = (AccountType)Enum.Parse(typeof(AccountType), accountTypeStr);
                            string owner = dict.ContainsKey("AccountOwner") ? dict["AccountOwner"].ToString() : "";
                            decimal balance = dict.ContainsKey("BalanceAmount") ? Convert.ToDecimal(dict["BalanceAmount"]) : 0;

                            if (accountType == AccountType.Checking)
                            {
                                decimal overdraft = dict.ContainsKey("OverdraftAmount") ? Convert.ToDecimal(dict["OverdraftAmount"]) : 0;
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

        // Realiza un depósito en la cuenta indicada
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

        // Realiza un retiro de la cuenta indicada
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

        // Muestra la información de la cuenta consultándola en json‑server.
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
