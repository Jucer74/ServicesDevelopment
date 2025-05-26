using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyBankService.Application.Dto;

namespace MoneyBankService.Application.Interfaces;

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

// Helper result classes
public class ServiceResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
    public static ServiceResult Ok() => new() { Success = true };
    public static ServiceResult Fail(string error) => new() { Success = false, ErrorMessage = error };
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    public static ServiceResult<T> Ok(T data) => new() { Success = true, Data = data };
    public new static ServiceResult<T> Fail(string error) => new() { Success = false, ErrorMessage = error };
}
