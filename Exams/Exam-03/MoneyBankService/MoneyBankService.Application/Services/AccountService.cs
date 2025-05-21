using System.Security.Principal;
using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Application.Interfaces.Services;
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

    public async Task<Account> CreateAsync(Account account)
    {
        bool accountExists = await AccountNumberExists(account.AccountNumber);

        if (accountExists)
        {
            throw new BadRequestException("Bad Request");
        }

        if (account.BalanceAmount <= 0)
        {
            throw new BadRequestException("Bad Request");
        }

        if (account.AccountType == 'C')
        {
            account.BalanceAmount += MAX_OVERDRAFT;
        }

        return await _accountRepository.AddAsync(account);

    }
    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _accountRepository.GetAllAsync();
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account is null)
            throw new NotFoundException($"Account [{id}] Not Found");

        return account;
    }

    public async Task<List<Account>> GetAccountByAccountNumber(string accountNumber)
    {
        return await _accountRepository.GetByAccountNumberAsync(accountNumber);
    }

    public async Task<Account> UpdateAsync(int id, Account account)
    {

        if (id != account.Id)
        {
            throw new BadRequestException("Bad Request");
        }

        bool accountExists = await AccountNumberExists(account.AccountNumber);

        if (!accountExists)
        {
            throw new BadRequestException("Account doesn't exists");
        }

        var oldAccount = await _accountRepository.GetByIdAsync(id);

        if (oldAccount is null)
        {
            throw new NotFoundException($"Account with Id=[{id}] Not Found");
        }

        await _accountRepository.UpdateAsync(account);

        return account;

    }

    public async Task DeleteAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);
        if (account == null)
        {
            throw new NotFoundException($"Account with Id=[{id}] Not Found");
        }

        await _accountRepository.RemoveAsync(account);
    }

    public async Task UpdateValue(int accountId, Transaction transaction, char transactionType)
    {

        bool accountNumberExist = await _accountRepository.ExistsAsync(a => a.AccountNumber == transaction.AccountNumber);

        if (!accountNumberExist)
        {
            throw new BadRequestException("Account doesn't exists");
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new NotFoundException();
        }


        if (accountId != transaction.Id)
        {
            throw new BadRequestException("The account id and the transaction id aren't the same");
        }

        if (transactionType == 'D')
        {

            UpdateDeposit(account, transaction);
        }

        if (transactionType == 'W')
        {

            if (account.BalanceAmount < transaction.ValueAmount)
            {
                throw new BadRequestException("Fondos Insuficientes");
            }

            UpdateWithdrawal(account, transaction);
        }

        await _accountRepository.UpdateAsync(account);
    }

    public static void UpdateDeposit(Account account, Transaction transaction)
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

    public static void UpdateWithdrawal(Account account, Transaction transaction)
    {
        account.BalanceAmount -= transaction.ValueAmount;

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
        {
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        }
    }

    private async Task<bool> AccountNumberExists(string accountNumber)
    {
        var accountExists = await _accountRepository.ExistsAsync(a => a.AccountNumber == accountNumber);
        return accountExists;
    }
}