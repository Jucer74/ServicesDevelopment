using MoneyBankService.Api.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;
namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const int MAX_OVERDRAFT = 1_000_000;

    public AccountService (IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<Account> CreateAccount(Account account)
    {
        if (account.AccountType == 'C')
        {
            account.BalanceAmount = GetInitalBalanceAmount(account.BalanceAmount);
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

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Account> UpdateAccount(int id, Account account)
    {
        if(id != account.Id)
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

    public async Task Deposit (int id, TransactionDto transactionDto) 
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account is null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        if (account.AccountNumber != transactionDto.AccountNumber)
        {
           throw new BadRequestException($"Account Number [{transactionDto.AccountNumber}] is different to Account.AccountNumber [{account.AccountNumber}]");
        }

        account.BalanceAmount += transactionDto.ValueAmount;

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

        await _accountRepository.UpdateAsync(account);

    }

    public async Task Withdraw(int id, TransactionDto transactionDto)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account is null)
        {
            throw new NotFoundException($"Account with Id={id} Not Found");
        }

        if (account.AccountNumber != transactionDto.AccountNumber)
        {
           throw new BadRequestException($"Account Number [{transactionDto.AccountNumber}] is different to Account.AccountNumber [{account.AccountNumber}]");
        }

        if (account.BalanceAmount < transactionDto.ValueAmount)
        {
            throw new BadRequestException($"Account Number [{transactionDto.AccountNumber}] has not enough balance to whithdraw [{transactionDto.ValueAmount}]");
        }

        account.BalanceAmount -= transactionDto.ValueAmount;

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
        {
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        }

        await _accountRepository.UpdateAsync(account);
    }   

    public async Task<IEnumerable<Account>> FindAccountsByAccountNumber(string accountNumber)
    {
        var accounts = await _accountRepository.FindAsync(a => a.AccountNumber == accountNumber);

        if (accounts.Any())
        {
            return accounts;
        }

        throw new NotFoundException($"Accounts not found");
    }

    private static decimal GetInitalBalanceAmount(decimal initalBalanceAmount)
    {
        return initalBalanceAmount += MAX_OVERDRAFT;
    }
}