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
        Task<IEnumerable<AccountDto>> GetAllAsync();
        Task<AccountDto?> GetByIdAsync(Guid id);
        Task<AccountDto> CreateAsync(AccountDto dto);
        Task<bool> UpdateAsync(AccountDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
