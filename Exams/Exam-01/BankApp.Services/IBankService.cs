using BankApp.Entities;

namespace BankApp.Services
{
    public interface IBankService
    {
        IBankAccount CreateAccount(AccountType accountType, string accountNumber, string accountOwner, decimal balanceAmount);
        IBankAccount GetBalance(string accountNumber);
        void DepositAccount(string accountNumber, decimal amount);
        void WithdrawalAccount(string accountNumber, decimal amount);
        bool ExistsAccount(string accountNumber);
    }
}
