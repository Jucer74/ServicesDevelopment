using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext context) : base(context)
    {
    }

    // Ejemplo de método específico para cuentas
    public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }
}