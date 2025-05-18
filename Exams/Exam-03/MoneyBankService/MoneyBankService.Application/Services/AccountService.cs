using MoneyBankService.Application.Exceptions;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const int MaxOverdraft = 1_000_000;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account> CreateAccount(Account account)
    {
        // Verificar si ya existe una cuenta con el mismo número
        var existingAccount = (await _accountRepository.GetAllAsync())
            .FirstOrDefault(a => a.AccountNumber == account.AccountNumber);
        
        if (existingAccount != null)
        {
            throw new BadRequestException($"La cuenta con el número {account.AccountNumber} ya existe.");
        }
        
        // Si la cuenta es de tipo corriente ('C'), sumar un millón al balance
        if (account.AccountType == 'C')
        {
            account.BalanceAmount += MaxOverdraft;
        }
        
        // Crear la cuenta si no existe
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
        
        // Verificar que el número de cuenta exista
        var existingAccount = (await _accountRepository.GetAllAsync())
            .FirstOrDefault(a => a.AccountNumber == account.AccountNumber);

        if (existingAccount is null)
        {
            throw new BadRequestException($"Account with AccountNumber [{account.AccountNumber}] No Exists");
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

    public async Task Deposit(int id, Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Transaction.Id [{transaction.Id}]");
        }
        
        // Validar que la cuenta exista
        
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account [{id}] Not Found");
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException($"La cuenta con el número {transaction.AccountNumber} no existe.");
        }
        
        // Actualizar el balance de la cuenta
        UpdateBalanceDeposit(account, transaction);
        await _accountRepository.UpdateAsync(account);
    }

    private void UpdateBalanceDeposit(Account account, Transaction transaction)
    {
        account.BalanceAmount += transaction.ValueAmount;
        if (account.AccountType == 'C')
        {
            if (account.OverdraftAmount > 0 && account.BalanceAmount < MaxOverdraft)
            {
                account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
            }
            else
            {
                account.OverdraftAmount = 0;
            }
        }
    }

    public async Task Withdraw(int id, Transaction transaction)
    {
        if (id != transaction.Id)
        {
            throw new BadRequestException($"Id [{id}] is different to Transaction.Id [{transaction.Id}]");
        }
        
        // Validar que la cuenta exista
        var account = await _accountRepository.GetByIdAsync(id);

        if (account == null)
        {
            throw new NotFoundException($"Account [{id}] Not Found");
        }

        if (account.AccountNumber != transaction.AccountNumber)
        {
            throw new BadRequestException($"La cuenta con el número {transaction.AccountNumber} no existe.");
        }
        
        if (account.BalanceAmount < transaction.ValueAmount)
        {
            throw new BadRequestException("Fondos Insuficientes");
        }
        
        // Actualizar el balance de la cuenta
        UpdateBalanceWithdraw(account, transaction);
        await _accountRepository.UpdateAsync(account);
    }

    private void UpdateBalanceWithdraw(Account account, Transaction transaction)
    {
        account.BalanceAmount -= transaction.ValueAmount;
        
        if (account.AccountType == 'C' && account.BalanceAmount < MaxOverdraft)
        {
            account.OverdraftAmount = MaxOverdraft - account.BalanceAmount;
        }
    }
    
    
    
}