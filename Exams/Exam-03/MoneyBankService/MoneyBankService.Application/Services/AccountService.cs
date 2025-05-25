using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Exceptions;
using MoneyBankService.Domain.Interfaces.Repositories;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private const decimal MaxOverdraft = 1_000_000;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<Account>> GetAllAccountsAsync(string? accountNumber = null)
    {
        return await _accountRepository.FindAsync(a =>
            string.IsNullOrEmpty(accountNumber) ||
            a.AccountNumber == accountNumber);
    }

    public async Task<Account?> GetAccountByIdAsync(int id)
    {
        return await _accountRepository.GetByIdAsync(id);
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        // 1) Verificar duplicado
        var existing = await _accountRepository.GetByAccountNumberAsync(account.AccountNumber);
        if (existing is not null)
            throw new BusinessException($"La Cuenta [{account.AccountNumber}] ya Existe");

        // 2) Validación de balance
        if (account.BalanceAmount <= 0)
            throw new BusinessException("El balance inicial debe ser mayor a cero");

        // 3) Sobregiro si es cuenta corriente
        if (account.AccountType == 'C')
            account.BalanceAmount += MaxOverdraft;

        return await _accountRepository.AddAsync(account);
    }

    public async Task<Account> UpdateAccountAsync(int id, Account account)
    {
        var existing = await _accountRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Cuenta no encontrada");

        // Si cambia número de cuenta, validar que exista
        if (account.AccountNumber != existing.AccountNumber)
        {
            var found = await _accountRepository.GetByAccountNumberAsync(account.AccountNumber);
            if (found is null)
                throw new BusinessException($"La Cuenta [{account.AccountNumber}] No Existe");
            existing.AccountNumber = account.AccountNumber;
        }

        // Actualizar datos
        existing.OwnerName = account.OwnerName;
        existing.AccountType = account.AccountType;
        existing.BalanceAmount = account.BalanceAmount;

        // Ajustar sobregiro si es corriente
        if (existing.AccountType == 'C')
        {
            existing.OverdraftAmount = existing.BalanceAmount < MaxOverdraft
                ? MaxOverdraft - existing.BalanceAmount
                : 0;
        }

        return await _accountRepository.UpdateAsync(existing);
    }


    public async Task DeleteAccountAsync(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Cuenta no encontrada");

        await _accountRepository.RemoveAsync(account);
    }

    public async Task<Account> DepositAsync(int id, decimal amount)
    {
        var account = await GetAndValidateAccount(id);
        account.BalanceAmount += amount;
        UpdateOverdraft(account);
        return await _accountRepository.UpdateAsync(account);
    }

    public async Task<Account> WithdrawAsync(int id, decimal amount)
    {
        var account = await GetAndValidateAccount(id);

        if (account.BalanceAmount < amount)
            throw new BusinessException("Fondos insuficientes");

        account.BalanceAmount -= amount;
        UpdateOverdraft(account);
        return await _accountRepository.UpdateAsync(account);
    }

    private async Task<Account> GetAndValidateAccount(int id)
    {
        var account = await _accountRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Cuenta no encontrada");
        return account;
    }

    private void UpdateOverdraft(Account account)
    {
        if (account.AccountType == 'C')
        {
            account.OverdraftAmount = account.BalanceAmount < MaxOverdraft
                ? MaxOverdraft - account.BalanceAmount
                : 0;
        }
    }
}