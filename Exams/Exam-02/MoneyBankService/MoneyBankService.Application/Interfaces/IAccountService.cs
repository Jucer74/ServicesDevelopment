using MoneyBankService.Domain.Entities;

namespace MoneyBankService.Application.Interfaces;

public interface IAccountService
{
    // Add Account
    public Task<Accounts> AddAccountAsync(Accounts account);

    // Get Account
    public Task<Accounts?> GetAccountAsync(string accountNumber);

    // Get Account by Id
    public Task<Accounts> GetAccountByIdAsync(int id);

    // Get All Accounts
    public Task<IEnumerable<Accounts>> GetAllAccountsAsync();

    // Update Account
    public Task<Accounts> UpdateAccountAsync(int id, Accounts account);

    // Delete Account
    public Task<Accounts> DeleteAccountAsync(int id);

    // Deposit
    public Task<Accounts> DepositAsync(int id, Transactions transaction);

    // Withdraw
    public Task<Accounts> WithdrawAsync(int id, Transactions transaction);
}