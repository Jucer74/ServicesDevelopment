using MoneyBankService.Domain.Models;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MoneyBankService.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
    public async Task<List<Account>> GetByNumberAccountAsync(string accountNumber)
    {
        return await _appDbContext.Accounts.Where(a => a.AccountNumber == accountNumber).ToListAsync();
    }
}