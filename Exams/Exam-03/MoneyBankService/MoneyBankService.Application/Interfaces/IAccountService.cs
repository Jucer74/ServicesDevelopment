using MoneyBankService.Api.Dto;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAllAsync();
    Task<AccountDto> GetByIdAsync(int id);
    Task<IEnumerable<AccountDto>> FindByAccountNumberAsync(string accountNumber);
    Task<AccountDto> CreateAsync(AccountDto dto);
    Task UpdateAsync(int id, AccountDto dto);
    Task DeleteAsync(int id);
    Task DepositAsync(int id, TransactionDto transaction);
    Task WithdrawAsync(int id, TransactionDto transaction);
}
