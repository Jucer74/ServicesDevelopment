using MoneyBankService.Domain.Entities;
using MoneyBankService.Domain.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;

namespace MoneyBankService.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}