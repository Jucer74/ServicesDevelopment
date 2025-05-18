using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Models;

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
        return account!;
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


        await _accountRepository.UpdateAsync(account);

        return account!;
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
    public async Task<Account?> GetAccountByNumberAccount(string accountNumber)
    {
        var account = await _accountRepository.FindAsync(x => x.AccountNumber == accountNumber);
        if (account is null)
        {
            throw new NotFoundException($"Account [{accountNumber}] Not Found");
        }
        return account.FirstOrDefault();
    }
}