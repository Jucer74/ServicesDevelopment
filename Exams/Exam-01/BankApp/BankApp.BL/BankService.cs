using BankApp.Entities;
using System;

namespace BankApp.BL
{
    public class BankService 
    {
        private readonly BankAccountRepository _repository;

        public BankService(BankAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<IBankAccount> CreateAccount(AccountType accountType, string accountNumber, string accountOwner, decimal balanceAmount)
        {
            IBankAccount newAccount;

            if (accountType == AccountType.Saving)
            {
                newAccount = new SavingAccount(accountNumber, accountOwner, balanceAmount);
            }
            else if (accountType == AccountType.Checking)
            {
                newAccount = new CheckingAccount(accountNumber, accountOwner, balanceAmount);
            }
            else
            {
                throw new ArgumentException("Invalid account type. Must be 1 (Saving) or 2 (Checking).");
            }

            await Task.Run(() => _repository.Create(newAccount)); // Simula una operación asíncrona
            return newAccount;
        }

        public async Task<IBankAccount> GetBalance(string accountNumber)
        {
            var account = await Task.Run(() => _repository.GetByAccountNumber(accountNumber));
            if (account == null)
                throw new InvalidOperationException($"Account {accountNumber} doesn't exist.");
            return account;
        }


        public async Task DepositAccount(string accountNumber, decimal amount)
        {
            var account = await GetBalance(accountNumber);
            account.Deposit(amount);
            await Task.Run(() => _repository.Update(account));
        }


        public async Task WithdrawalAccount(string accountNumber, decimal amount)
        {
            var account = await GetBalance(accountNumber);
            account.Withdrawal(amount);
            await Task.Run(() => _repository.Update(account));
        }


        public bool ExistsAccount(string accountNumber)
        {
            return _repository.Exists(accountNumber);
        }
    }
}
