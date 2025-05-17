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
            // Si no se proporciona AccountNumber, devuelve todas las cuentas
            return (await _accountRepository.GetAllAsync()).ToList();
        }

        return (await _accountRepository.GetAllAsync()).Where(account => account.AccountNumber == accountNumber).ToList();
    }
    private const decimal MAX_OVERDRAFT = 1000000m;

    public async Task<Account> DepositAsync(int accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId)
            ?? throw new NotFoundException($"Cuenta con ID {accountId} no encontrada");

        account.BalanceAmount += amount;

        if (account.AccountType == 'C') // Cuenta Corriente
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

        await _accountRepository.UpdateAsync(account);
        return account;
    }

    public async Task<Account> WithdrawAsync(int accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId)
            ?? throw new NotFoundException($"Cuenta con ID {accountId} no encontrada");

        if (account.AccountType == 'A') // Cuenta de Ahorros
        {
            if (account.BalanceAmount >= amount)
            {
                account.BalanceAmount -= amount;
            }
            else
            {
                throw new BusinessException("Fondos insuficientes.");
            }
        }
        else if (account.AccountType == 'C') // Cuenta Corriente
        {
            if (account.BalanceAmount + account.OverdraftAmount >= amount)
            {
                account.BalanceAmount -= amount;

                if (account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
                {
                    account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
                }
                else
                {
                    account.OverdraftAmount = 0;
                }
            }
            else
            {
                throw new BusinessException("Fondos insuficientes.");
            }
        }

        await _accountRepository.UpdateAsync(account);
        return account;
    }
}