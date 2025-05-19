using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync() =>
            await _context.Accounts.ToListAsync();

        public async Task<Account?> GetByIdAsync(Guid id) =>
            await _context.Accounts.FindAsync(id);

        public async Task<Account> CreateAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;
            _context.Accounts.Remove(account);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
