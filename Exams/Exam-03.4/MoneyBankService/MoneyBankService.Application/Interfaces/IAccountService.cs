using MoneyBankService.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoneyBankService.Application.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<IEnumerable<AccountDto>> GetAccountsByAccountNumberAsync(string accountNumber);
        Task<AccountDto?> GetAccountByIdAsync(int id);
        Task<AccountDto> CreateAccountAsync(AccountDto accountDto);
        Task<AccountDto> UpdateAccountAsync(int id, AccountDto accountDto);
        Task<bool> DeleteAccountAsync(int id);
        Task<bool> DepositAsync(int id, TransactionDto transactionDto);
        Task<bool> WithdrawAsync(int id, TransactionDto transactionDto);
    }
}