using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Api.Dto;

public class TransactionDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    public String ValueAmount { get; set; } = null!;
}
