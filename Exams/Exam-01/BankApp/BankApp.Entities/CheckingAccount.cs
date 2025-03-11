
namespace BankApp.Entities
{
    public class CheckingAccount : IBankAccount
    {
        private const decimal MIN_OVERDRAFT_AMOUNT = 1_000_000m;
        private decimal _actualDeposit;

        public string AccountNumber { get; private set; }
        public string AccountOwner { get; private set; }
        public decimal OverdraftAmount { get; private set; }

        public AccountType AccountType => AccountType.Checking;

        public decimal BalanceAmount => _actualDeposit + (MIN_OVERDRAFT_AMOUNT - OverdraftAmount);

        public CheckingAccount(string accountNumber, string accountOwner, decimal initialDeposit)
        {
            if (accountNumber.Length != 10 || !accountNumber.All(char.IsDigit))
                throw new ArgumentException("Account number must have 10 digits.");

            if (string.IsNullOrWhiteSpace(accountOwner) || accountOwner.Length > 50)
                throw new ArgumentException("Account owner is required and max length is 50 characters.");

            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            _actualDeposit = initialDeposit;
            OverdraftAmount = 0;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be greater than zero.");

            if (OverdraftAmount > 0)
            {
                if (amount >= OverdraftAmount)
                {
                    amount -= OverdraftAmount;
                    OverdraftAmount = 0;
                }
                else
                {
                    OverdraftAmount -= amount;
                    amount = 0;
                }
            }

            _actualDeposit += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be greater than zero.");

            decimal availableFunds = _actualDeposit + (MIN_OVERDRAFT_AMOUNT - OverdraftAmount);
            if (amount > availableFunds)
                throw new InvalidOperationException("Insufficient funds.");

            if (amount <= _actualDeposit)
            {
                _actualDeposit -= amount;
            }
            else
            {
                decimal remainder = amount - _actualDeposit;
                _actualDeposit = 0;
                OverdraftAmount += remainder;
            }
        }
    }
}
