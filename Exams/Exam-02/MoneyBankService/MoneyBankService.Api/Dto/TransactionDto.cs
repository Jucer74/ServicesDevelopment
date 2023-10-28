using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Api.Dto;

public class TransactionDto
{
    public int Id { get; set; }
    public string AccountNumber { get; set; } = null!;
    
    // Se Corrije para poder probar
    public decimal ValueAmount { get; set; }
}
