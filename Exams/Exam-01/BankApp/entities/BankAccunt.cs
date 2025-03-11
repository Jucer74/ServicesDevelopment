
namespace BankApp.entities
{

    public class BankAccount
    {
        public string AccountNumber  { get; set; }
        public string AccountOwner { get; set; }
        public decimal BalanceAmount { get; set; }

        public int AccountType { get; set; }

        public decimal OverdraftAmount { get; set; }


        public BankAccount(string accountNumber, string accountOwner, decimal balanceAmount, int accountType ,decimal overdraftAmount)
        {
            AccountNumber = accountNumber;
            AccountOwner = accountOwner;
            BalanceAmount = balanceAmount;
            AccountType = accountType;
            OverdraftAmount = overdraftAmount;
        }

    }

}