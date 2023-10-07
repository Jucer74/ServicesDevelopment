using System;
using System.Transactions;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private const int MAX_OVERDRAFT = 1_000_000;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            if (await AccountNumberExistsAsync(account.AccountNumber))
            {
                throw new BadRequestException($"The account [{account.AccountNumber}] already exists.");
            }

            if (account.BalanceAmount <= 0)
            {
                throw new BadRequestException("The account balance must be greater than zero.");
            }

            if (account.AccountType == 'C')
            {
                account.BalanceAmount += MAX_OVERDRAFT;
            }

            return await _accountRepository.AddAsync(account);
        }

        public async Task DeleteAccount(int id)
        {
            var original = await _accountRepository.GetByIdAsync(id);

            if (original != null)
            {
                await _accountRepository.RemoveAsync(original);
                return;
            }

            throw new NotFoundException($"Account with Id={id} not found.");
        }

        public async Task<Account> GetAccountById(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);

            if (account == null)
            {
                throw new NotFoundException();
            }

            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAsync();
        }

        public async Task<Account> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Account.Id [{account.Id}].");
            }
            if (account.BalanceAmount <= 0)
            {
                throw new BadRequestException("The balance must be greater than zero.");
            }

            var original = await _accountRepository.GetByIdAsync(id);

            if (original != null)
            {
                return await _accountRepository.UpdateAsync(account);
            }

            throw new NotFoundException($"Account with Id={id} not found.");
        }

        public async Task<Account> DepositToAccount(int id, Domain.Entities.Transaction transaction)
        {
            if (id != transaction.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Account.Id [{transaction.Id}].");
            }

            if (!await AccountExists(id))
            {
                throw new NotFoundException();
            }

            var account = await FindAccount(id);

            if (account.AccountNumber != transaction.AccountNumber)
            {
                throw new BadRequestException("The account number is different from the transaction.");
            }

            UpdateDepositValue(account, transaction);

            return await _accountRepository.UpdateAsync(account);
        }

        public async Task<Account> WithdrawalToAccount(int id, Domain.Entities.Transaction transaction)
        {
            if (id != transaction.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Account.Id [{transaction.Id}].");
            }

            if (!await AccountExists(id))
            {
                throw new NotFoundException();
            }

            var account = await FindAccount(id);

            if (!AccountHasSufficientFunds(account, transaction))
            {
                throw new BadRequestException("Insufficient funds.");
            }

            if (account.AccountNumber != transaction.AccountNumber)
            {
                throw new BadRequestException("The account number is different from the transaction.");
            }

            UpdateWithdrawalValue(account, transaction);

            return await _accountRepository.UpdateAsync(account);
        }

        private async Task<bool> AccountExists(int id)
        {
            var accounts = await _accountRepository.GetAllAsync() as List<Account>;
            return accounts.Exists(account => account.Id == id);
        }

        private async Task<Account> FindAccount(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        private async Task<bool> AccountNumberExistsAsync(string accountNumber)
        {
            var accounts = await _accountRepository.GetAllAsync() as List<Account>;
            return accounts.Exists(account => account.AccountNumber == accountNumber);
        }

        private bool AccountHasSufficientFunds(Account account, Domain.Entities.Transaction transaction)
        {
            return account.BalanceAmount >= transaction.ValueAmount;
        }

        private void UpdateDepositValue(Account account, Domain.Entities.Transaction transaction)
        {
            account.BalanceAmount += transaction.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
                else
                {
                    account.OverdraftAmount = 0;
                }
            }
        }

        private void UpdateWithdrawalValue(Account account, Domain.Entities.Transaction transaction)
        {
            account.BalanceAmount -= transaction.ValueAmount;

            if (account.AccountType == 'C' && account.OverdraftAmount >= 0 && account.BalanceAmount < MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
        }

        public Task<Account> WithDrawalToAccount(int id, Domain.Entities.Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
