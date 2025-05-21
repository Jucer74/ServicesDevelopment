using Microsoft.EntityFrameworkCore;
using MoneyBankService.Application.Dto;
using MoneyBankService.Application.Interfaces;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Application.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private const decimal MAX_OVERDRAFT = 1000000m;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync(string? accountNumber)
    {
        var query = _context.Accounts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(accountNumber))
        {
            query = query.Where(a => a.AccountNumber == accountNumber);
        }

        var accounts = await query.ToListAsync();

        return accounts.Select(a => new AccountDto
        {
            Id = a.Id,
            AccountNumber = a.AccountNumber,
            AccountType = a.AccountType,
            OwnerName = a.OwnerName,
            BalanceAmount = a.BalanceAmount,
            OverdraftAmount = a.OverdraftAmount,
            CreationDate = a.CreationDate
        });
    }

    public async Task<AccountDto> GetAccountByIdAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id) 
                      ?? throw new Exception("Cuenta no encontrada");

        return new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            AccountType = account.AccountType,
            OwnerName = account.OwnerName,
            BalanceAmount = account.BalanceAmount,
            OverdraftAmount = account.OverdraftAmount,
            CreationDate = account.CreationDate
        };
    }

    public async Task<AccountDto> CreateAccountAsync(AccountDto dto)
    {
        if (dto.BalanceAmount <= 0)
            throw new Exception("El Balance debe ser mayor a cero");

        var account = new Account
        {
            AccountNumber = dto.AccountNumber,
            AccountType = dto.AccountType,
            OwnerName = dto.OwnerName,
            CreationDate = dto.CreationDate
        };

        if (dto.AccountType == 'C')
        {
            account.BalanceAmount = dto.BalanceAmount + MAX_OVERDRAFT;
            account.OverdraftAmount = MAX_OVERDRAFT;
        }
        else
        {
            account.BalanceAmount = dto.BalanceAmount;
        }

        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        dto.Id = account.Id;
        dto.OverdraftAmount = account.OverdraftAmount;
        dto.BalanceAmount = account.BalanceAmount;
        return dto;
    }

    public async Task UpdateAccountAsync(int id, AccountDto dto)
    {
        var account = await _context.Accounts.FindAsync(id) 
                      ?? throw new Exception("Cuenta no encontrada");

        account.AccountNumber = dto.AccountNumber;
        account.AccountType = dto.AccountType;
        account.OwnerName = dto.OwnerName;
        account.BalanceAmount = dto.BalanceAmount;
        account.OverdraftAmount = dto.OverdraftAmount;
        account.CreationDate = dto.CreationDate;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id) 
                      ?? throw new Exception("Cuenta no encontrada");

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }

    public async Task DepositAsync(int id, TransactionDto dto)
    {
        var account = await _context.Accounts.FindAsync(id)
                      ?? throw new Exception("Cuenta no encontrada");

        account.BalanceAmount += dto.ValueAmount;

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

        await _context.SaveChangesAsync();
    }

    public async Task WithdrawAsync(int id, TransactionDto dto)
    {
        var account = await _context.Accounts.FindAsync(id) 
                      ?? throw new Exception("Cuenta no encontrada");

        if (dto.ValueAmount > account.BalanceAmount)
            throw new Exception("Fondos Insuficientes");

        account.BalanceAmount -= dto.ValueAmount;

        if (account.AccountType == 'C' && account.OverdraftAmount > 0 && account.BalanceAmount < MAX_OVERDRAFT)
        {
            account.OverdraftAmount = MAX_OVERDRAFT - account.BalanceAmount;
        }

        await _context.SaveChangesAsync();
    }
}
