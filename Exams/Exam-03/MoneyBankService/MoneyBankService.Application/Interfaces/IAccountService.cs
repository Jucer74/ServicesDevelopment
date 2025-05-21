using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<AccountDto>> GetAccountsAsync(string? accountNumber);
    Task<AccountDto> GetAccountByIdAsync(int id);
    Task<AccountDto> CreateAccountAsync(AccountDto dto);
    Task UpdateAccountAsync(int id, AccountDto dto);
    Task DeleteAccountAsync(int id);
    Task DepositAsync(int id, TransactionDto dto);
    Task WithdrawAsync(int id, TransactionDto dto);
}
