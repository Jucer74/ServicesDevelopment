using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Application.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAccount(Account account)
    {
        var existingAccounts = await _accountRepository.GetAllAsync();

        if (existingAccounts.Any(a => a.AccountNumber == account.AccountNumber))
        {
            throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] already exists");
        }

        await _accountRepository.AddAsync(account);
        return account;
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

        var originalAccount = await _accountRepository.GetByIdAsync(id);

        if (originalAccount is null)
        {
            throw new NotFoundException($"Account [{id}] Not Found");
        }

        await _accountRepository.UpdateAsync(account);
        return account;
    }

    public async Task DeleteAccount(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account is null)
        {
            throw new NotFoundException($"Account [{id}] Not Found");
        }

        await _accountRepository.RemoveAsync(account);
    }

    public async Task<List<Account>> GetAccounts(string? accountNumber = null)
    {
        if (string.IsNullOrEmpty(accountNumber))
        {
          
            return (await _accountRepository.GetAllAsync()).ToList();
        }

        return (await _accountRepository.GetAllAsync()).Where(account => account.AccountNumber == accountNumber).ToList();
    }



}