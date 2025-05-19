namespace MoneyBankService.Api.Dto;

public class TransactionDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public decimal ValueAmount { get; set; }
}
