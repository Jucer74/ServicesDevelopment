using BankApp.Entities;
using System;

namespace BankApp.Services
{
    public class BankService : IBankService
    {
        private readonly IBankAccountRepository _repository;

        public BankService(IBankAccountRepository repository)
        {
            _repository = repository;
        }

        public IBankAccount CreateAccount(AccountType accountType, string accountNumber, string accountOwner, decimal balanceAmount)
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

            _repository.Create(newAccount);
            return newAccount;
        }

        public IBankAccount GetBalance(string accountNumber)
        {
            var account = _repository.GetByAccountNumber(accountNumber);
            if (account == null)
                throw new InvalidOperationException($"Account {accountNumber} doesn't exist.");
            return account;
        }

        public void DepositAccount(string accountNumber, decimal amount)
        {
            var account = GetBalance(accountNumber);
            account.Deposit(amount);
            _repository.Update(account);
        }

        public void WithdrawalAccount(string accountNumber, decimal amount)
        {
            var account = GetBalance(accountNumber);
            account.Withdrawal(amount);
            _repository.Update(account);
        }

        public bool ExistsAccount(string accountNumber)
        {
            return _repository.Exists(accountNumber);
        }
    }
}
