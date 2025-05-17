namespace MoneyBankService.Application.Dto;

public class AccountDto
{
    public int Id { get; set; }
    public char AccountType { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public decimal BalanceAmount { get; set; }
    public decimal OverdraftAmount { get; set; }
}
