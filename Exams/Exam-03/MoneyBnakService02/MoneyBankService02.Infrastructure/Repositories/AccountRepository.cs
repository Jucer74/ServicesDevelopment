using MoneyBankService02.Domain.Entities;
using MoneyBankService02.Domain.Interfaces.Repositories;
using MoneyBankService02.Infrastructure.Common;
using MoneyBankService02.Infrastructure.Context;

namespace MoneyBankService02.Infrastructure.Repositories;

public class AccountRepository : Repository<BankAccount>, IAccountRepository
{
    public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
