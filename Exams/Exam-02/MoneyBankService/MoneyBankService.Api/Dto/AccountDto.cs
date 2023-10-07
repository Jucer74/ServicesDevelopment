using System.ComponentModel.DataAnnotations;

namespace MoneyBankService.Api.Dto;

public class AccountDto
{
    public int Id { get; set; }
    public String AccountType { get; set; } = "A";
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string AccountNumber { get; set; } = null!;
    public string OwnerName { get; set; } = null!;
    public String BalanceAmount { get; set; } = null!;
    public String OverdraftAmount { get; set; } = null!;
}
