namespace MoneyBankService.Api.Dto;

public class AccountDto
{
    public int Id { get; set; }
    public string AccountType { get; set; } = null!;
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public string AccountNumber { get; set; } = null!;
    public string OwnerName { get; set; } = null!;
    public string  BalanceAmount { get; set; } = null!;
    public string OverdraftAmount { get; set; } = null!;
}