namespace BankApp.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public required string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
