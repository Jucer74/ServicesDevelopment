using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBankService.Api.Dto;

namespace MoneyBankService.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<AccountDto> CreateAccountAsync(AccountDto accountDto);
        Task<bool> UpdateAccountAsync(int id, AccountDto accountDto);
        Task<bool> DeleteAccountAsync(int id);
    }
}
