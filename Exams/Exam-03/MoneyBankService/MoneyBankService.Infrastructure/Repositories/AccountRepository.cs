using Microsoft.EntityFrameworkCore;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
    public async Task<List<Account>> GetByAccountNumberAsync(string accountNumber)
    {
        return await _appDbContext.Accounts.Where(a => a.AccountNumber == accountNumber).ToListAsync();
    } 
}