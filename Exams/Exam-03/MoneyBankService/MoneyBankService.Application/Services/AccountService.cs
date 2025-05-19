using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Application.Interfaces.Services;
using MoneyBankService.Domain.Constants;
using MoneyBankService.Domain.Entities;

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

        bool accountNumberExist = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == account.AccountNumber);

        if (accountNumberExist)
        {
            throw new BadRequestException();
        }

        var accountIdExist = await _accountRepository.GetByIdAsync(account.Id);

        if (accountIdExist != null)
        {
            throw new BadRequestException();
        }

        if (account.BalanceAmount <= 0)
        {
            throw new BadRequestException("El Balance debe ser mayor a cero"); 
        }

        if (account.AccountType == 'C')
        {
            account.BalanceAmount += Constants.MaxOverdraft;
        }

        return await _accountRepository.AddAsync(account);
    }

    public async Task<List<Account>> GetAccountByNumber(string accountNumber)
    {
        return await _accountRepository.GetByAccountNumberAsync(accountNumber);
    }

    public async Task<List<Account>> GetAllAccounts()
    {
        return (await _accountRepository.GetAllAsync()).ToList();
    }
    public async Task<Account> GetAccountById(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id=[{id}] Not Found");
        }

        return account;
    }

    public async Task<Account> UpdateAccount(int id, Account account)
    {
        if (id != account.Id)
        {
            throw new BadRequestException("Bad Request");
        }

        bool accountNumberExist = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == account.AccountNumber);

        if (!accountNumberExist)
        {
            throw new BadRequestException();
        }

        var original = await _accountRepository.GetByIdAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Account with Id=[{id}] Not Found");
        }

        await _accountRepository.UpdateAsync(account);

        return account;
    }

    public async Task DeleteAccount(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account with Id=[{id}] Not Found");
        }

        await _accountRepository.RemoveAsync(account);
    }

    public async Task UpdateDepositValue(int accountId, Transaction transaction)
    {
        if (accountId != transaction.Id)
        {
            throw new BadRequestException();
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new NotFoundException();
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException();
        }

        UpdateDeposit(account, transaction);

        await _accountRepository.UpdateAsync(account);
    }

    public async Task UpdateWithdrawalValue(int accountId, Transaction transaction)
    {
        
        if (accountId != transaction.Id)
        {
            throw new BadRequestException();
        }

        bool accountNumberExist = await _accountRepository.ExistsByPropertyAsync(a => a.AccountNumber == transaction.AccountNumber);

        if (!accountNumberExist)
        {
            throw new BadRequestException();
        }

        var account = await _accountRepository.GetByIdAsync(accountId);

        if (account == null)
        {
            throw new NotFoundException();
        }

        if (account.BalanceAmount < transaction.ValueAmount)
        {
            throw new BadRequestException("Fondos Insuficientes");
        }

        UpdateWithdrawal(account, transaction);

        await _accountRepository.UpdateAsync(account);
    }

    private void UpdateDeposit(Account account, Transaction transaction)
    {
        account.BalanceAmount += transaction.ValueAmount;

        if (account.AccountType == 'C')
        {
            if (account.OverdraftAmount > 0 && account.BalanceAmount < Constants.MaxOverdraft)
            {
                account.OverdraftAmount = Constants.MaxOverdraft - account.BalanceAmount;
            }
            else
            {
                account.OverdraftAmount = 0;
            }
        }
    }
    private void UpdateWithdrawal(Account account, Transaction transaction)
    {
        account.BalanceAmount -= transaction.ValueAmount;

        if (account.AccountType == 'C' && account.BalanceAmount < Constants.MaxOverdraft)
        {
            account.OverdraftAmount = Constants.MaxOverdraft - account.BalanceAmount;
        }
    }
}