using MoneyBankService.Application.Common;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAccountsAsync(string? accountNumber);
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<ServiceResult<AccountDto>> CreateAccountAsync(AccountDto accountDto);
        Task<ServiceResult> UpdateAccountAsync(AccountDto accountDto);
        Task<ServiceResult> DeleteAccountAsync(int id);
        Task<ServiceResult> DepositAsync(int id, TransactionDto transactionDto);
        Task<ServiceResult> WithdrawalAsync(int id, TransactionDto transactionDto);
    }
}