using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
}