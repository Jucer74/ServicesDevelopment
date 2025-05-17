using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Models;
using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

        public async Task<Account?> GetByAccountNumber(string accountNumber)
        {
            return await _appDbContext.Account
                .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }
    }
}