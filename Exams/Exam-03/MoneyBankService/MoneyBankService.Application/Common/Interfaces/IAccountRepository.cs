using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Common.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
