using System;
using System.Threading.Tasks;
using System.Text.Json;

using BankApp.DAL;
using BankApp.Entities;

namespace BankApp.BL
{
    public class BankService
    {
        private readonly BankRepository _repository;
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "http://localhost:3000/accounts";

        public BankService()
        {
            _repository = new BankRepository();
        }

        public async Task CreateAccountAsync(IBankAccount account)
        {
            var existingAccount = await _repository.GetAccountByNumberAsync(account.AccountNumber);
            if (existingAccount != null)
                throw new InvalidOperationException("Account already exists.");

            await _repository.CreateAccountAsync(account);
        }

        public async Task<decimal> GetBalanceAsync(string accountNumber)
        {
            var account = await _repository.GetAccountByNumberAsync(accountNumber);

            if (account == null)
                throw new InvalidOperationException("Account not found.");

            return account.BalanceAmount;
        }

        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _repository.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            account.Deposit(amount);
            await _repository.UpdateAccountAsync(account);
        }

        public async Task WithdrawalAsync(string accountNumber, decimal amount)
        {
            var account = await _repository.GetAccountByNumberAsync(accountNumber);
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            if (account.Withdrawal(amount))
            {
                await _repository.UpdateAccountAsync(account);
            }
            else
            {
                throw new InvalidOperationException("Insufficient funds.");
            }
        }
    }
}