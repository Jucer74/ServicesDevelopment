using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;
using System.Transactions;
using Transaction = MoneyBankService.Domain.Entities.Transaction;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const decimal MAX_OVERDRAFT = 1000000.00M;
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAccount(Account account)
    {

        if (account.AccountType == 'A') 
        {
            // El balance ya es el valor que seproporciono inicialmente
        }
        
        else if (account.AccountType == 'C')  
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        return await _accountRepository.AddAsync(account);
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
        var account = await _accountRepository.GetByIdAsync(id);

        if (account is not null)
        {
            return account;
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }
    public async Task<IEnumerable<Account>> GetAccountsByAccountNumberAsync(string accountNumber)
    {

            var accounts = await _accountRepository.FindAsync(account => account.AccountNumber == accountNumber);
            return accounts;


    }


    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Account> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Account.Id [{account.Id}]");
        }

        var original = await _accountRepository.GetByIdAsync(id);

        if (original is not null)
        {
            return await _accountRepository.UpdateAsync(account);
        }

        throw new NotFoundException($"Account with Id={id} Not Found");
    }

    public async Task Deposit(int id, Transaction transaction)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        account.BalanceAmount += transaction.ValueAmount;

        if (account.AccountType == 'C')
        {
            if (account.BalanceAmount < MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
            else
            {
                account.OverdraftAmount = 0; 
            }
        }

        await _accountRepository.UpdateAsync(account);  
    }


    public async Task Withdrawal(int id, Transaction transaction)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        decimal totalAvailableFunds = account.BalanceAmount + (MAX_OVERDRAFT - account.OverdraftAmount);

        if (transaction.ValueAmount > totalAvailableFunds)
        {
            throw new BadRequestException("Fondos Insuficientes");
        }

        if (transaction.ValueAmount > account.BalanceAmount)
        {
            decimal requiredOverdraft = transaction.ValueAmount - account.BalanceAmount;

            if (account.AccountType == 'C' && requiredOverdraft <= MAX_OVERDRAFT - account.OverdraftAmount)
            {
                account.OverdraftAmount += requiredOverdraft;
                account.BalanceAmount = 0;
            }
            else
            {
                throw new BadRequestException("Fondos Insuficientes");
            }
        }
        else
        {
            account.BalanceAmount -= transaction.ValueAmount;

            if (account.AccountType == 'C' && account.BalanceAmount < MAX_OVERDRAFT)
            {
                account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
            }
        }

        await _accountRepository.UpdateAsync(account);
    }









}


