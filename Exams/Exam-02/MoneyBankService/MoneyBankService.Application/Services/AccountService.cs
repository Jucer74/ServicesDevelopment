using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public Task<Account> CreateAccount(Account account)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAccount(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccountById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAsync();
    }

    public Task<Account> UpdateAccount(int id, Account account)
    {
        throw new NotImplementedException();
    }
}