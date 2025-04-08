using BankApp.entities;
using System.Text.Json;
using System.Text;
using System.Net;

namespace BankApp.bankService
{
    public class BankService
    {
        public static async Task<BankAccount> GetAccount(string accountNumber)
        {
            using HttpClient client = new HttpClient();

            string ApiUrl = ConfigManager.GetApiUrl() + $"/accounts/{accountNumber}";

            HttpResponseMessage response = await client.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var account = JsonSerializer.Deserialize<BankAccount>(responseBody);

                if (account != null)
                {
                    return account;
                }

                throw new Exception("La cuenta no existe");
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException($"La cuenta no existe: {response.StatusCode}");
            }
            else
            {
                throw new HttpRequestException($"Error en el servidor: {response.StatusCode}");
            }
        }

        public static async Task<bool> ExistsAccount(string accountNumber)
        {
            using HttpClient client = new HttpClient();

            string ApiUrl = ConfigManager.GetApiUrl() + $"/accounts/{accountNumber}";

            HttpResponseMessage response = await client.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var accountList = JsonSerializer.Deserialize<BankAccount>(responseBody);

                // Implente Retorno directo
                //if (accountList != null)
                //{
                //    return true;
                //}
                //return false;

                return (accountList != null);

            }
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public static async Task<BankAccount> CreateAccount(BankAccount bankAccount)
        {
            bool accountExists = await ExistsAccount(bankAccount.AccountNumber);

            if (accountExists)
            {
                throw new Exception("La cuenta ya existe");
            }

            using HttpClient client = new HttpClient();

            string ApiUrl = ConfigManager.GetApiUrl() + $"/accounts";

            string jsonAccount = JsonSerializer.Serialize(bankAccount);

            StringContent content = new StringContent(jsonAccount, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ApiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return bankAccount;
            }
            else
            {
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
            }
        }

        public static async Task<decimal> GetBalanceAccount(string accountNumber)
        {
            bool accountExists = await ExistsAccount(accountNumber);

            if (accountExists)
            {
                BankAccount bankAccount = await GetAccount(accountNumber);

                decimal balanceAmount = bankAccount.BalanceAmount + bankAccount.OverdraftAmount;

                return balanceAmount;
            }

            throw new Exception("La cuenta no existe");
        }

        public static async Task<BankAccount> DepositAmount(string accountNumber, decimal amountValue)
        {
            bool accountExists = await ExistsAccount(accountNumber);

            if (accountExists)
            {
                BankAccount account = await GetAccount(accountNumber);

                if (account.AccountType == 2)
                {
                    if (account.OverdraftAmount < 1000000)
                    {
                        decimal overdraftDebt = 1000000 - account.OverdraftAmount;

                        if (overdraftDebt >= amountValue)
                        {
                            account.OverdraftAmount += amountValue;
                        }
                        else
                        {
                            decimal amountValueForBalance = amountValue - overdraftDebt;

                            account.OverdraftAmount += overdraftDebt;

                            account.BalanceAmount += amountValueForBalance;
                        }
                    }
                    else
                    {
                        account.BalanceAmount += amountValue;
                    }
                }
                else
                {
                    account.BalanceAmount += amountValue;
                }

                using HttpClient client = new HttpClient();

                string ApiUrl = ConfigManager.GetApiUrl() + $"/accounts/{account.id}";

                string jsonAccount = JsonSerializer.Serialize(account);

                StringContent content = new StringContent(jsonAccount, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return account;
                }
                else
                {
                    throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
                }
            }

            throw new Exception("La cuenta no existe");
        }

        public static async Task<BankAccount> Withdraw(string accountNumber, decimal amount)
        {
            bool accountExists = await ExistsAccount(accountNumber);

            if (accountExists)
            {
                BankAccount account = await GetAccount(accountNumber);

                if (account.AccountType == 1)
                {
                    if (account.BalanceAmount >= amount)
                    {
                        account.BalanceAmount -= amount;
                    }
                    else
                    {
                        throw new Exception("La cuenta no tiene saldo suficiente");
                    }
                }
                else if (account.AccountType == 2)
                {
                    decimal totalAccountBalance = account.BalanceAmount + account.OverdraftAmount;

                    bool isTotalAccountBalanceEnough = totalAccountBalance >= amount;

                    if (isTotalAccountBalanceEnough)
                    {
                        bool isAccountBalanceEnough = account.BalanceAmount >= amount;

                        if (isAccountBalanceEnough)
                        {
                            account.BalanceAmount -= amount;
                        }
                        else
                        {
                            decimal residualDebt = amount - account.BalanceAmount;

                            account.BalanceAmount = 0;

                            account.OverdraftAmount -= residualDebt;
                        }
                    }
                    else
                    {
                        throw new Exception("La cuenta no tiene saldo suficiente");
                    }
                }

                using HttpClient client = new HttpClient();

                string ApiUrl = ConfigManager.GetApiUrl() + $"/accounts/{account.id}";

                string jsonAccount = JsonSerializer.Serialize(account);

                StringContent content = new StringContent(jsonAccount, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(ApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return account;
                }
                else
                {
                    throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
                }
            }

            throw new Exception("La cuenta no existe");
        }
    }
}