using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Api.Dto;

public class AccountDto
{
    public int Id { get; set; }
    public String AccountType { get; set; } = "A";
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string AccountNumber { get; set; } = null!;
    public string OwnerName { get; set; } = null!;
    
    // Los Valores deben ser Decimal
    public decimal BalanceAmount { get; set; } 
    public decimal OverdraftAmount { get; set; }
}
