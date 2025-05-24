namespace MoneyBankService.Application.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal ValueAmount { get; set; }
    }
}