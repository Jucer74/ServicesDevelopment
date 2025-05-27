using System.Security.Principal;
using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    const decimal MAX_OVERDRAFT = 1_000_000m;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return (await _accountRepository.GetAllAsync()).ToList();
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {

        bool existsAccountNumber = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == account.AccountNumber);
        bool existsId = await _accountRepository.ExistsByPropertyAsync(a => a.Id == account.Id);

        if (existsAccountNumber || existsId)
        {
            throw new BadRequestException("El id o el numero de cuenta ya existen");
        }

        if (account.BalanceAmount < 0)
        {
            throw new BadRequestException("El Balance debe ser mayor a cero");
        }

        if (account.AccountType == 'C')
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        return await _accountRepository.AddAsync(account);
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new NotFoundException($"Account with Id=[{account}] Not Found");
        }

        await _accountRepository.RemoveAsync(account);
    }

    public async Task<Account> UpdateAccount(int accountId, Account newAccount)
    {
        if (accountId != newAccount.Id)
        {
            throw new BadRequestException();
        }

        bool existsAccountNumber = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == newAccount.AccountNumber);

        if (!existsAccountNumber)
        {
            throw new BadRequestException($"La Cuenta [{accountId}] No Existe");
        }

        return  await _accountRepository.UpdateAsync(newAccount);

    }

    public async Task<Account> GetAccountByIdAsync(int accountId)
    {
        var account = await  _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new NotFoundException($"Account with Id=[{account}] Not Found");
        }

        return account;
    }

    public async Task<List<Account>> GetAccountsByAccountNumberAsync(string accountNumber)
    {
        return (await _accountRepository.FindAsync(ac => ac.AccountNumber == accountNumber)).ToList();
    }

    public async Task DepositAsync(int accountId, Transaction transaction)
    {
        if (accountId != transaction.Id)
        {
            throw new BadRequestException();
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new NotFoundException();
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transaccion");
        }

        UpdateDepositValue(account, transaction);

        await _accountRepository.UpdateAsync(account);
    }

    public async Task WithdrawAsync(int accountId, Transaction transaction)
    {
        if (accountId != transaction.Id)
        {
            throw new BadRequestException();
        }

        bool existsAccountNumber = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == transaction.AccountNumber);

        if (!existsAccountNumber)
        {
            throw new BadRequestException();
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account is null)
        {
            throw new NotFoundException();
        }

        if (account.BalanceAmount < transaction.ValueAmount)
        {
            throw new BadRequestException("Fondos incuficientes");
        }

        UpdateWithdrawalValue(account, transaction);

        await _accountRepository.UpdateAsync(account);
    }

    public void UpdateDepositValue(Account account, Transaction transaction)
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

    public void UpdateWithdrawalValue(Account account, Transaction transaction)
    {
        account.BalanceAmount -= transaction.ValueAmount;

        if (account.AccountType == 'C' && account.BalanceAmount < MAX_OVERDRAFT)
        {
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        }

    }
}