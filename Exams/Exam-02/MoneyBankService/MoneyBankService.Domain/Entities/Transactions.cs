namespace MoneyBankService.Domain.Entities;

public class Transactions
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal ValueAmount { get; set; }
}