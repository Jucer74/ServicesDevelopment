using System;
using System.Transactions;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{

    private readonly IAccountRepository _accountRepository;
    private const int MAX_OVERDRAFT = 1_000_000;


    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAcount(Account account)
    {

        if (ExistAccountNumberAsync(account.AccountNumber).Result)
        {
            throw new BadRequestException($"La Cuenta [{account.AccountNumber}] ya Existe");
        }

        if (account.BalanceAmount <= 0)
        {
            throw new BadRequestException("El Balance debe ser mayor a cero");
        }

        if (account.AccountType == 'C')
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        return await _accountRepository.AddAsync(account);
        throw new NotImplementedException();
    }

    public async Task DeleteAccount(int id)
    {
        var original = await _accountRepository.GetByIdAsync(id);

        if (original is not null)
        {
            await _accountRepository.RemoveAsync(original);
            return;
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }

    public async Task<Account> GetAccountById(int id)
    {
        
        var account = await _accountRepository.GetByIdAsync(id) as Account;

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

    public async Task<Account> UpdateAcount(int id, Account account)
    {
        if (id != account.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Account.Id [{account.Id}]");
        }
        if (account.BalanceAmount <= 0)
        {
            throw new BadRequestException("El Balance debe ser mayor a cero");
        }

        var original = await _accountRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _accountRepository.UpdateAsync(account);
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }


    public async Task<Account> DepositToAccount(int id, Domain.Entities.Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Account.Id [{transaction.Id}]");
        }

        if (!AccountExists(id).Result)
        {
            throw new NotFoundException();
        }

        var account = FindAccount(id).Result;

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transaccion");
        }

        UpdateDepositValue(account, transaction);

        return await _accountRepository.UpdateAsync(account);
    }

    public async Task<Account> WithDrawalToAccount(int id, Domain.Entities.Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Account.Id [{transaction.Id}]");
        }

        if (!AccountExists(id).Result)
        {
            throw new NotFoundException();
        }

        var account = FindAccount(id).Result;

        if (!AccountHasSufficientFunds(account, transaction))
        {
            throw new BadRequestException("Fondos Insuficientes");
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException("El Numero de la Cuenta es Diferente al de la transaccion");
        }

        UpdateWithdrawalValue(account, transaction);

        return await _accountRepository.UpdateAsync(account);
    }


    

    private async Task<bool> AccountExists(int id)
    {
        Boolean exist = false;
        var accounts = await _accountRepository.GetAllAsync() as List<Account>;
        foreach (var account in accounts!)
        {
            if (account.Id == id)
            {
                exist = true;
            }
        }
        return exist;
    }

    
    private async Task<Account> FindAccount(int id)
    {
        return await _accountRepository.GetByIdAsync(id) as Account;
    }

    private async Task<bool> ExistAccountNumberAsync(string accountNumber)
    {
        Boolean exist=false;
        var accounts = await _accountRepository.GetAllAsync() as List<Account>;
        foreach (var account in accounts!) {
            if(account.AccountNumber == accountNumber)
            {
                exist = true;
            }
        }
        return exist;
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
}