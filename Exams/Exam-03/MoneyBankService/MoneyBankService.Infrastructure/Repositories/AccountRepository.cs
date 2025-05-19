using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _dbContext;

        public AccountRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _dbContext.Accounts.FindAsync(id);
        }

        public async Task<Account> CreateAsync(Account account)
        {
            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<bool> UpdateAsync(int id, Account account)
        {
            var existingAccount = await _dbContext.Accounts.FindAsync(id);
            if (existingAccount == null)
            {
                return false;
            }

            existingAccount.Name = account.Name;
            existingAccount.Balance = account.Balance;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var account = await _dbContext.Accounts.FindAsync(id);
            if (account == null)
            {
                return false;
            }

            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}