using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private const int OverdraftLimit = 1_000_000;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            if (await _repository.GetByIdAsync(account.Id) is not null)
            {
                throw new BadRequestException($"Account with Id [{account.Id}] already exists.");
            }

            if (await _repository.GetByAccountNumber(account.AccountNumber) is not null)
            {
                throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] already exists.");
            }

            if (account.BalanceAmount <= 0)
            {
                throw new BadRequestException("El Balance debe ser mayor a cero");
            }

            if (account.AccountType == 'C')
            {
                account.BalanceAmount += OverdraftLimit;
            }

            await _repository.AddAsync(account);
            return account;
        }

        public async Task<Account> GetAccountById(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            return account ?? throw new NotFoundException($"Account [{id}] Not Found");
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            var result = await _repository.GetAllAsync();
            return result.ToList();
        }

        public async Task<Account?> GetAccountByAccountNumber(string accountNumber)
        {
            return await _repository.GetByAccountNumber(accountNumber);
        }

        public async Task<Account> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Account.Id [{account.Id}]");
            }

            if (await _repository.GetByIdAsync(id) is null)
            {
                throw new NotFoundException($"Account [{id}] Not Found");
            }

            if (await _repository.GetByAccountNumber(account.AccountNumber) is null)
            {
                throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] already exists.");
            }

            await _repository.UpdateAsync(account);
            return account;
        }

        public async Task DeleteAccount(int id)
        {
            var target = await _repository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"Account [{id}] Not Found");

            await _repository.RemoveAsync(target);
        }

        public async Task Deposit(int id, Transaction transaction)
        {
            ValidateTransactionId(id, transaction.Id);

            var account = await _repository.GetByIdAsync(id)
                          ?? throw new NotFoundException($"Account with Id [{id}] not found");

            ValidateTransactionAmount(transaction.ValueAmount);
            ValidateAccountMatch(account.AccountNumber, transaction.AccountNumber);

            account.BalanceAmount += transaction.ValueAmount;

            if (account.AccountType == 'C')
            {
                account.OverdraftAmount = CalculateOverdraft(account.BalanceAmount);
            }

            await _repository.UpdateAsync(account);
        }

        public async Task Withdraw(int id, Transaction transaction)
        {
            ValidateTransactionId(id, transaction.Id);

            var account = await _repository.GetByIdAsync(id)
                          ?? throw new NotFoundException($"Account [{id}] Not Found");

            ValidateAccountMatch(account.AccountNumber, transaction.AccountNumber);
            ValidateTransactionAmount(transaction.ValueAmount);

            if (account.BalanceAmount < transaction.ValueAmount)
            {
                throw new BadRequestException("Not enough balance");
            }

            account.BalanceAmount -= transaction.ValueAmount;

            if (account.AccountType == 'C')
            {
                account.OverdraftAmount = CalculateOverdraft(account.BalanceAmount);
            }

            await _repository.UpdateAsync(account);
        }

        #region Helpers

        private static void ValidateTransactionId(int id, int transactionId)
        {
            if (id != transactionId)
            {
                throw new BadRequestException($"Id [{id}] is different from Transaction.Id [{transactionId}]");
            }
        }

        private static void ValidateTransactionAmount(decimal value)
        {
            if (value <= 0)
            {
                throw new BadRequestException($"Transaction Amount [{value}] must be greater than 0");
            }
        }

        private static void ValidateAccountMatch(string accountNumber, string transactionAccountNumber)
        {
            if (accountNumber != transactionAccountNumber)
            {
                throw new BadRequestException($"Account number [{transactionAccountNumber}] does not match with Account [{accountNumber}]");
            }
        }

        private static decimal CalculateOverdraft(decimal balance)
        {
            return balance < OverdraftLimit ? OverdraftLimit - balance : 0;
        }

        #endregion
    }
}
