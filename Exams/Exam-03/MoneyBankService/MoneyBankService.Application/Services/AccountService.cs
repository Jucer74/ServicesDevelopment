using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const int MAX_OVERDRAFT = 1_000_000;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<Account>> GetAccounts(string? accountNumber = null)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
            return (await _accountRepository.GetAllAsync()).ToList();
        }

        return (await _accountRepository.GetAllAsync())
            .Where(account => account.AccountNumber == accountNumber)
            .ToList();
    }

    public async Task<Account> CreateAccount(Account account)
    {
        if (await AccountExists(account.AccountNumber))
        {
            throw new BadRequestException("Bad Request", new[] { $"La cuenta con el número {account.AccountNumber} ya existe." });
        }

        if (account.AccountType == 'C')
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        await _accountRepository.AddAsync(account);
        return account;
    }

    public async Task<Account> GetAccountById(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account is null)
        {
            throw new NotFoundException("Not Found", new[] { $"Account [{id}] Not Found" });
        }
        return account;
    }

    public async Task<Account> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            throw new BadRequestException("Bad Request", new[] { $"Id [{id}] is different to Account.Id [{account.Id}]" });
        }

        var originalAccount = await _accountRepository.GetByIdAsync(id);
        if (originalAccount is null)
        {
            throw new NotFoundException("Not Found", new[] { $"Account [{id}] Not Found" });
        }

        var existing = (await _accountRepository.GetAllAsync())
            .FirstOrDefault(a => a.AccountNumber == account.AccountNumber);

        if (existing == null || existing.Id != id)
        {
            throw new BadRequestException("Bad Request", new[] { $"Account with AccountNumber [{account.AccountNumber}] No Exists or belongs to another account." });
        }

        await _accountRepository.UpdateAsync(account);
        return account;
    }

    public async Task DeleteAccount(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account is null)
        {
            throw new NotFoundException("Not Found", new[] { $"Account with Id=[{id}] Not Found" });
        }

        await _accountRepository.RemoveAsync(account);
    }

    public async Task Deposit(int id, Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException("Bad Request", new[] { $"Id [{id}] is different to Transaction.Id [{transaction.Id}]" });
        }

        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NotFoundException("Not Found", new[] { $"Account with Id=[{id}] Not Found" });
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("Bad Request", new[] { $"Transaction.AccountNumber no coincide con la cuenta" });
        }

        UpdateDeposit(account, transaction);
        await _accountRepository.UpdateAsync(account);
    }

    public async Task Withdraw(int id, Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException("Bad Request", new[] { $"Id [{id}] is different to Transaction.Id [{transaction.Id}]" });
        }

        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NotFoundException("Not Found", new[] { $"Account with Id=[{id}] Not Found" });
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("Bad Request", new[] { $"Transaction.AccountNumber no coincide con la cuenta" });
        }

        if (account.BalanceAmount < transaction.ValueAmount)
        {
            throw new BadRequestException("Bad Request", new[] { "Fondos Insuficientes" });
        }

        UpdateWithdrawal(account, transaction);
        await _accountRepository.UpdateAsync(account);
    }

    private async Task<bool> AccountExists(string accountNumber)
    {
        var existingAccount = (await _accountRepository.GetAllAsync())
            .FirstOrDefault(a => a.AccountNumber == accountNumber);
        return existingAccount != null;
    }

    private void UpdateDeposit(Account account, Transaction transaction)
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

    private void UpdateWithdrawal(Account account, Transaction transaction)
    {
        account.BalanceAmount -= transaction.ValueAmount;

        if (account.AccountType == 'C' && account.BalanceAmount < MAX_OVERDRAFT)
        {
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        }
    }
}
