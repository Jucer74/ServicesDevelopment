using System.Threading.Tasks;
using BankApp.DAL;
using BankApp.Entities;

namespace BankApp.BL
{
    public class BankAccountService
    {
        private readonly BankAccountRepository _bankAccountRepository;

        public BankAccountService()
        {
            _bankAccountRepository = new BankAccountRepository();
        }

        public async Task CreateAccount(BankAccount account)
        {
            await _bankAccountRepository.AddAccount(account);
        }

        public async Task<BankAccount?> GetAccount(string accountNumber)
        {
            return await _bankAccountRepository.GetAccount(accountNumber);
        }

        public async Task Deposit(string accountNumber, decimal amount)
        {
            var account = await _bankAccountRepository.GetAccount(accountNumber);
            if (account != null)
            {
                account.BalanceAmount += amount;
                await _bankAccountRepository.UpdateAccount(account);
            }
            else
            {
                throw new InvalidOperationException("Account not found.");
            }
        }

        public async Task Withdraw(string accountNumber, decimal amount)
        {
            var account = await _bankAccountRepository.GetAccount(accountNumber);
            if (account != null)
            {
                if (account.AccountType == AccountType.Checking && account.BalanceAmount + account.OverdraftAmount >= amount)
                {
                    account.BalanceAmount -= amount;
                }
                else if (account.AccountType == AccountType.Saving && account.BalanceAmount >= amount)
                {
                    account.BalanceAmount -= amount;
                }
                else
                {
                    throw new InvalidOperationException("Insufficient funds.");
                }
                await _bankAccountRepository.UpdateAccount(account);
            }
            else
            {
                throw new InvalidOperationException("Account not found.");
            }
        }
    }
}