namespace MoneyBankService.Api.Dto;

public class TransactionDto
{

    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;

    // El Valor debe ser un Decimal
    public decimal ValueAmount { get; set; } 
}