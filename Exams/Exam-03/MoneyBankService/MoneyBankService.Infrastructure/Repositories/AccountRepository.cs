using MoneyBankService.Application.Interfaces.Repositories;
using MoneyBankService.Infrastructure.Common;
using MoneyBankService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MoneyBankService.Domain.Entities;


namespace MoneyBankService.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
    
}