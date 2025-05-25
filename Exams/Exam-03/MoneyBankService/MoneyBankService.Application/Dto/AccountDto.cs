﻿namespace MoneyBankService.Application.Dtos;

public class AccountDto
{
    public int Id { get; set; }
    public char AccountType { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;

    public string AccountNumber { get; set; } = null!;

    public string OwnerName { get; set; } = null!;

    public decimal BalanceAmount { get; set; }

    public decimal OverdraftAmount { get; set; }
}
