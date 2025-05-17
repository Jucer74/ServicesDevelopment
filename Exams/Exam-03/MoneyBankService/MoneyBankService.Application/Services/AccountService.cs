using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Models;

namespace MoneyBankService.Application.Services
{
    public class AccountService : IAccountService
    {
       
        private readonly IAccountRepository _accountRepository;
        private const int MaxOverdraft = 1_000_000;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var existingAccount = await _accountRepository.GetByIdAsync(account.Id);
            if (existingAccount != null)
            {
                throw new BadRequestException($"Account with Id [{account.Id}] already exists.");
            }
            var exisintAccountNumber = await _accountRepository.GetByAccountNumber(account.AccountNumber);
            if (exisintAccountNumber != null)
            {
                throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] already exists.");
            }
            if (account.BalanceAmount < 0)
            {
                throw new BadRequestException("El Balance debe ser mayor a cero");
            }
            if(account.AccountType == 'C')
            {
                account.BalanceAmount += MaxOverdraft;
            }

            await _accountRepository.AddAsync(account);

            return account;
        }

        public async Task DeleteAccount(int id)
        {
            var original = await _accountRepository.GetByIdAsync(id);

            if (original is null)
            {
                throw new NotFoundException($"Account [{id}] Not Found");
            }

            await _accountRepository.RemoveAsync(original);
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return (await _accountRepository.GetAllAsync()).ToList();
        }

        public async Task<Account> GetAccountById(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account is null)
            {
                throw new NotFoundException($"Account [{id}] Not Found");
            }

            return account;
        }

        public async Task<Account> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Account.Id [{account.Id}]");
            }
            var original = await _accountRepository.GetByIdAsync(id);
            if (original is null)
            {
                throw new NotFoundException($"Account [{id}] Not Found");
            }
            var existAccountNumber = await _accountRepository.GetByAccountNumber(account.AccountNumber);
            if (existAccountNumber == null)
            {
                throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] already exists.");
            }

            await _accountRepository.UpdateAsync(account);
            return account;
        }
        public async Task<Account?> GetAccountByAccountNumber(string accountNumber)
        {
            return await _accountRepository.GetByAccountNumber(accountNumber);
          
        }

        public async Task Deposit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                throw new BadRequestException($"Id [{id}] is different from Transaction.Id [{transaction.Id}]");
            }

            var account = await _accountRepository.GetByIdAsync(id);
            if (account is null)
            {
                throw new NotFoundException($"Account with Id [{id}] not found");
            }

            
            if (transaction.ValueAmount <= 0)
            {
                throw new BadRequestException($"Transaction Amount [{transaction.ValueAmount}] must be greater than 0");
            }

            if (account.AccountNumber != transaction.AccountNumber)
            {
                throw new BadRequestException($"Account number [{transaction.AccountNumber}] does not match with Account [{account.AccountNumber}]");
            }

            account.BalanceAmount += transaction.ValueAmount;

            if (account.AccountType == 'C')
            {
                if (account.OverdraftAmount > 0 && account.BalanceAmount < MaxOverdraft)
                {
                    account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
                }
                else
                {
                    account.OverdraftAmount = 0;
                }
            }

            await _accountRepository.UpdateAsync(account);
        }


        public async Task Withdraw(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                throw new BadRequestException($"Id [{id}] is different to Transaction.Id [{transaction.Id}]");
            }

            var account = await _accountRepository.GetByIdAsync(id);
            
            if (account is null)
            {
                throw new NotFoundException($"Account [{id}] Not Found");
            }
            if (account.AccountNumber != transaction.AccountNumber)
            {
                throw new BadRequestException($"Account number [{transaction.AccountNumber}] does not match with Account [{account.AccountNumber}]");
            }

            if (transaction.ValueAmount <= 0)
            {
                throw new BadRequestException($"Transaction Amount [{transaction.ValueAmount}] must be greater than 0");
            }

            if (account.BalanceAmount < transaction.ValueAmount)
            {
                throw new BadRequestException("Not enough balance");
            }

            account.BalanceAmount -= transaction.ValueAmount;

            if (account.AccountType == 'C' && account.BalanceAmount < MaxOverdraft)
            {
                account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
            }


            await _accountRepository.UpdateAsync(account);
        }


     
    }
}
